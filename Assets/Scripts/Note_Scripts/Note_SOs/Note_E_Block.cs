using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Block_Next_Attack")]
public class Note_E_Block : Note_SO
{
    [SerializeField] private int blockTime;
    [SerializeField] private int upgradeBlockTime;
    public override void Apply(Combat_Handler combatHandler, Note_SO note)
    {
        combatHandler.SetBlock(note, GetTotalStat(Game_Manager.instance.statsManager.noteLevelTracker.GetStackCount(note)));
    }

    public override int GetTotalStat(int ownedCount)
    {
        return blockTime + upgradeBlockTime * Mathf.Max(0, ownedCount - 1);
    }
}
