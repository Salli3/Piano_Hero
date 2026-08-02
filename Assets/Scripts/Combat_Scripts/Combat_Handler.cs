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

    [SerializeField] private int enemyBlock;
    [SerializeField] private int enemyStackingDamage;

    private IHealth GetTargetHP(bool isHostile) => isHostile ? playerHP : enemyHP;
    private IHitNumber GetHitNumberUI(bool isHostile) => isHostile ? playerUI : enemyUI;

    private void OnEnable()
    {
        Enemy_HP.OnEnemyDefeated += RefreshEnemyStatus;
    }
    private void OnDisable()
    {
        Enemy_HP.OnEnemyDefeated -= RefreshEnemyStatus;
    }

    private void Start()
    {
        UpdateCombatStatus();
    }

    private void UpdateCombatStatus()
    {
        playerUI.UpdateCombatStatusUI(playerBlock, playerStackingDamage);
        enemyUI.UpdateCombatStatusUI(enemyBlock, enemyStackingDamage);
    }

    private void RefreshEnemyStatus(Enemy_SO _)
    {
        enemyBlock = 0;
        enemyStackingDamage = 0;
        UpdateCombatStatus();
    }

    //Deal Damage
    public void DealDamage(Note_SO note, int damage)
    {
        if (Block(note.isHostile))
        {
            StopAllCoroutines();
            return;
        }

        int finalDamage = note.isHostile ? damage : damage + Game_Manager.instance.statsManager.Damage;
        GetTargetHP(note.isHostile).ChangeHP(finalDamage);
    }

    //Heal
    public void Heal(Note_SO note, int damage)
    {
        GetTargetHP(note.isHostile).ChangeHP(-damage);
    }

    //Block
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

    //Clear note
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

    //Stack damage
    public int StackDamage(Note_SO note, int amount)
    {
        ref int stackingDamage = ref (note.isHostile ? ref enemyStackingDamage : ref playerStackingDamage);
        stackingDamage += amount;
        UpdateCombatStatus();
        return stackingDamage;
    }

    //Multi hit
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
}
