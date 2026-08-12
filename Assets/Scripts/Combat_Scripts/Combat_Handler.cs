using System.Collections;
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
    private int PlayerDamage(int damage)
    {
        return damage + Game_Manager.instance.statsManager.Damage;
    }

    private int EnemyDamage(int damage)
    {
        return damage * Game_Manager.instance.enemyDamageMultiplier;
    }

    public void DealDamageToPlayer()
    {
        if (Block(true)) return;

        playerHP.ChangeHP(-EnemyDamage(enemyHP.CurrentEnemy.enemyDamage));
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
        GetTargetHP(isHostile).ChangeHP(-finalDamage);
    }

    public void SelfDamage(bool isHostile, int damage)
    {
        if (Block(!isHostile))
        {
            StopAllCoroutines();
            return;
        }
        int finalDamage = isHostile ? EnemyDamage(damage) : PlayerDamage(damage);
        GetTargetHP(!isHostile).ChangeHP(-finalDamage);
    }
    #endregion

    #region Heal
    public void Heal(bool isHostile, int damage)
    {
        GetTargetHP(!isHostile).ChangeHP(damage);
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
        GetHitNumberUI(isHostile).ShowHitNumber(0, true);
        UpdateCombatStatus();
        return true;
    }
    public void RemoveBlock(bool isHostile, int amount)
    {
        ref int block = ref (isHostile ? ref playerBlock : ref enemyBlock);

        block -= amount;
        if (block <= 0) block = 0;
        GetHitNumberUI(isHostile).ShowHitNumber(0, true);
        UpdateCombatStatus();
    }
    #endregion

    #region Attack boost
    public void SetAttackBoost(bool isHostile, int amount)
    {
        ref int boost = ref (isHostile ? ref enemyBoostAttack : ref playerBoostAttack);
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
    public int StackDamage(bool isHostile, int amount)
    {
        ref int stackingDamage = ref (isHostile ? ref enemyStackingDamage : ref playerStackingDamage);
        stackingDamage += amount;
        UpdateCombatStatus();
        return stackingDamage;
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
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    #endregion
}
