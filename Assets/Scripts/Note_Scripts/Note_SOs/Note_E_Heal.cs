using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Heal")]
public class Note_E_Heal : Note_SO
{
    [SerializeField] private int healAmount;
    [SerializeField] private int upgradeHeal;

    public override void Apply(Combat_Handler combatHandler, Note_SO note)
    {
        combatHandler.Heal(note, GetTotalStat(Game_Manager.instance.statsManager.noteLevelTracker.GetStackCount(note)));
    }

    public override int GetTotalStat(int ownedCount)
    {
        return healAmount + upgradeHeal * Mathf.Max(0, ownedCount - 1);
    }
}
