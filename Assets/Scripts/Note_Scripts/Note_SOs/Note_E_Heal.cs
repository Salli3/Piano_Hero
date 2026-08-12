using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Heal")]
public class Note_E_Heal : Note_SO
{
    [SerializeField] private int healAmount;
    [SerializeField] private int upgradeHeal;

    public override void Apply(Combat_Handler combatHandler, int level)
    {
        combatHandler.Heal(isHostile, GetTotalStat(level));
    }

    public override int GetTotalStat(int level)
    {
        return healAmount + upgradeHeal * Mathf.Max(0, level - 1);
    }
}
