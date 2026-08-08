using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Combat_Handler : MonoBehaviour
{
    [SerializeField] private Player_HP playerHP;
    [SerializeField] private Player_UI playerUI;
    [SerializeField] private Enemy_HP enemyHP;
    [SerializeField] private Enemy_UI enemyUI;

    [SerializeField] private int playerBlock;
    [SerializeField] private int playerStackingDamage;
    [SerializeField] private int playerBoostAttack;

    [SerializeField] private int enemyBlock;
    [SerializeField] private int enemyStackingDamage;
    [SerializeField] private int enemyBoostAttack;

    private IHealth GetTargetHP(bool isHostile) => isHostile ? playerHP : enemyHP;
    private IHitNumber GetHitNumberUI(bool isHostile) => isHostile ? playerUI : enemyUI;

    private void OnEnable() => Enemy_HP.OnEnemyDefeated += RefreshEnemyStatus;
    private void OnDisable() => Enemy_HP.OnEnemyDefeated -= RefreshEnemyStatus;

    private void Start()
    {
        UpdateCombatStatus();
    }

    private void UpdateCombatStatus()
    {
        playerUI.UpdateCombatStatusUI(playerBlock, playerStackingDamage, playerBoostAttack);
        enemyUI.UpdateCombatStatusUI(enemyBlock, enemyStackingDamage, enemyBoostAttack);
    }

    private void RefreshEnemyStatus(Enemy_SO _)
    {
        enemyBlock = 0;
        enemyStackingDamage = 0;
        UpdateCombatStatus();
    }

    #region Deal damage
    public void DamagePlayer()
    {
        if (Block(true)) return;

        playerHP.ChangeHP(enemyHP.CurrentEnemy.enemyDamage);
    }

    public void DealDamage(Note_SO note, int damage)
    {
        if (Block(note.isHostile))
        {
            StopAllCoroutines();
            return;
        }
        int finalDamage = note.isHostile ? damage * Game_Manager.instance.enemyDamageMultiplier : damage + Game_Manager.instance.statsManager.Damage;
        if (BoostAttack(note.isHostile)) finalDamage *= 2;
        GetTargetHP(note.isHostile).ChangeHP(finalDamage);
    }

    public void SelfDamage(Note_SO note, int damage)
    {
        if (Block(note.isHostile))
        {
            StopAllCoroutines();
            return;
        }
        int finalDamage = note.isHostile ? damage * Game_Manager.instance.enemyDamageMultiplier : damage + Game_Manager.instance.statsManager.Damage;
        GetTargetHP(!note.isHostile).ChangeHP(finalDamage);
    }
    #endregion

    #region Heal
    public void Heal(Note_SO note, int damage)
    {
        GetTargetHP(!note.isHostile).ChangeHP(-damage);
    }
    #endregion

    #region Block
    public void SetBlock(Note_SO note, int amount)
    {
        ref int block = ref (note.isHostile ? ref enemyBlock : ref playerBlock);
        block = amount;
        UpdateCombatStatus();
    }
    public bool Block(bool isHostile)
    {
        ref int block = ref (isHostile ? ref playerBlock : ref enemyBlock);
        if (block <= 0) return false;

        block--;
        GetHitNumberUI(isHostile).ShowHitNumber(0, true);
        UpdateCombatStatus();
        return true;
    }
    public void RemoveBlock(Note_SO note, int amount)
    {
        ref int block = ref (note.isHostile ? ref playerBlock : ref enemyBlock);

        block -= amount;
        if (block <= 0) block = 0;
        GetHitNumberUI(note.isHostile).ShowHitNumber(0, true);
        UpdateCombatStatus();
    }
    #endregion

    #region Attack boost
    public void SetAttackBoost(Note_SO note, int amount)
    {
        ref int boost = ref (note.isHostile ? ref enemyBoostAttack : ref playerBoostAttack);
        boost = amount;
        UpdateCombatStatus();
    }
    public bool BoostAttack(bool isHostile)
    {
        ref int boost = ref (isHostile ? ref enemyBoostAttack : ref playerBoostAttack);
        if (boost <= 0) return false;

        boost--;
        UpdateCombatStatus();
        return true;
    }
    #endregion

    #region Clear note
    public int ClearNote()
    {
        Note[] allNotes = FindObjectsByType<Note>(FindObjectsSortMode.None);
        int noteLayer = LayerMask.NameToLayer("Note");
        int clearedCount = 0;

        foreach (Note note in allNotes)
        {
            if (note.gameObject.layer == noteLayer && note.noteSO.isHostile)
            {
                clearedCount++;
                note.OnNoteHit();
            }
        }
        return clearedCount;
    }
    #endregion

    #region Stack damage
    public int StackDamage(Note_SO note, int amount)
    {
        ref int stackingDamage = ref (note.isHostile ? ref enemyStackingDamage : ref playerStackingDamage);
        stackingDamage += amount;
        UpdateCombatStatus();
        return stackingDamage;
    }
    #endregion

    #region Multi hit
    public void RunMultiHit(Note_SO note, int damage, int hitTime)
    {
        StartCoroutine(AttackInterval(note, damage, hitTime));
    }
    private IEnumerator AttackInterval(Note_SO note, int damage, int hitTime)
    {
        for (int i = 0; i < hitTime; i++)
        {
            DealDamage(note, damage);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    #endregion
}
