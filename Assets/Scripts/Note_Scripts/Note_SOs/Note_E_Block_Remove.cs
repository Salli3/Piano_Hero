using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Block_Remove")]
public class Note_E_Block_Remove : Note_SO
{
    [SerializeField] private int removeAmount;
    [SerializeField] private int upgradeRemoveAmount;
    public override void Apply(Combat_Handler combatHandler, Note_SO note)
    {
        combatHandler.RemoveBlock(note, GetTotalStat(Game_Manager.instance.statsManager.GetStackCount(note)));
    }

    public override int GetTotalStat(int ownedCount)
    {
        return removeAmount + upgradeRemoveAmount * Mathf.Max(0, ownedCount - 1);
    }
}
