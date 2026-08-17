using System;
using System.Collections;
using UnityEngine;

public class Combat_Manager : MonoBehaviour
{
    [SerializeField] private int playerBlock;
    [SerializeField] private int playerDamageStack;
    [SerializeField] private int playerAttackBoost;

    [SerializeField] private int enemyBlock;
    [SerializeField] private int enemyDamageStack;
    [SerializeField] private int enemyAttackBoost;

    public static event Action<int, bool> DamagePlayer;
    public static event Action<Status> PlayerStatusChange;

    public static event Action<int, bool> DamageEnemy;
    public static event Action<Status> EnemyStatusChange;

    private void OnEnable() => Enemy_HP.OnEnemyDefeated += OnEnemyDefeat;
    private void OnDisable() => Enemy_HP.OnEnemyDefeated -= OnEnemyDefeat;

    private void Start()
    {
        UpdateCombatStatus();
    }

    private void UpdateCombatStatus()
    {
        PlayerStatusChange?.Invoke(new Status(playerBlock, playerDamageStack, playerAttackBoost));
        EnemyStatusChange?.Invoke(new Status(enemyBlock, enemyDamageStack, enemyAttackBoost));
    }

    private void OnEnemyDefeat(Enemy_SO enemySO)
    {
        RefreshEnemyStatus();
    }

    private void RefreshEnemyStatus()
    {
        enemyBlock = 0;
        enemyDamageStack = 0;
        enemyAttackBoost = 0;
        UpdateCombatStatus();
    }

    #region Deal damage
    private int PlayerDamage(int damage)
    {
        return damage + Game_Manager.instance.statsManager.Damage;
    }

    private int EnemyDamage(int damage)
    {
        return Mathf.RoundToInt(damage * Game_Manager.instance.EnemyDamageMultiplier);
    }

    public void DealDamageToPlayer()
    {
        if (Block(true)) return;

        DamagePlayer?.Invoke(-EnemyDamage(Game_Manager.instance.CurrentEnemy.enemyDamage), false);
    }

    public void DealDamage(bool isHostile, int damage)
    {
        if (Block(isHostile))
        {
            StopAllCoroutines();
            return;
        }
        int finalDamage = isHostile ? EnemyDamage(damage) : PlayerDamage(damage);
        if (BoostAttack(isHostile)) finalDamage *= 2;

        (isHostile? DamagePlayer : DamageEnemy)?.Invoke(-finalDamage, false);
    }

    public void SelfDamage(bool isHostile, int damage)
    {
        if (Block(!isHostile))
        {
            StopAllCoroutines();
            return;
        }
        int finalDamage = isHostile ? EnemyDamage(damage) : PlayerDamage(damage);
        (!isHostile ? DamagePlayer : DamageEnemy)?.Invoke(-finalDamage, false);
    }
    #endregion

    #region Heal
    public void Heal(bool isHostile, int amount)
    {
        (!isHostile ? DamagePlayer : DamageEnemy)?.Invoke(amount, false);
    }
    #endregion

    #region Block
    public void SetBlock(bool isHostile, int amount)
    {
        ref int block = ref (isHostile ? ref enemyBlock : ref playerBlock);
        block = amount;
        UpdateCombatStatus();
    }
    public bool Block(bool isHostile)
    {
        ref int block = ref (isHostile ? ref playerBlock : ref enemyBlock);
        if (block <= 0) return false;

        block--;
        (isHostile ? DamagePlayer : DamageEnemy)?.Invoke(0, true);
        UpdateCombatStatus();
        return true;
    }
    public void RemoveBlock(bool isHostile, int amount)
    {
        ref int block = ref (isHostile ? ref playerBlock : ref enemyBlock);

        block -= amount;
        if (block <= 0) block = 0;
        (isHostile ? DamagePlayer : DamageEnemy)?.Invoke(0, true);
        UpdateCombatStatus();
    }
    #endregion

    #region Attack boost
    public void SetAttackBoost(bool isHostile, int amount)
    {
        ref int boost = ref (isHostile ? ref enemyAttackBoost : ref playerAttackBoost);
        boost = amount;
        UpdateCombatStatus();
    }
    public bool BoostAttack(bool isHostile)
    {
        ref int boost = ref (isHostile ? ref enemyAttackBoost : ref playerAttackBoost);
        if (boost <= 0) return false;

        boost--;
        UpdateCombatStatus();
        return true;
    }
    #endregion

    #region Clear note
    public int ClearNote(bool isHostile)
    {
        Note[] allNotes = FindObjectsByType<Note>(FindObjectsSortMode.None);
        int noteLayer = LayerMask.NameToLayer("Note");
        int clearedCount = 0;

        foreach (Note note in allNotes)
        {
            if (note.gameObject.layer == noteLayer && isHostile != note.noteSO.isHostile)
            {
                clearedCount++;
                note.OnNoteHit();
            }
        }
        return clearedCount;
    }
    #endregion

    #region Stack damage
    public int StackDamage(bool isHostile)
    {
        ref int stack = ref (isHostile ? ref enemyDamageStack : ref playerDamageStack);
        stack ++;
        UpdateCombatStatus();
        return stack;
    }
    #endregion

    #region Multi hit
    public void RunMultiHit(bool isHostile, int damage, int hitTime)
    {
        StartCoroutine(AttackInterval(isHostile, damage, hitTime));
    }
    private IEnumerator AttackInterval(bool isHostile, int damage, int hitTime)
    {
        for (int i = 0; i < hitTime; i++)
        {
            DealDamage(isHostile, damage);
            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion
}
