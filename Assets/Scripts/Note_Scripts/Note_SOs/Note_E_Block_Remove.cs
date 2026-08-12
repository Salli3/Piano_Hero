using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Block_Remove")]
public class Note_E_Block_Remove : Note_SO
{
    [SerializeField] private int removeAmount;
    [SerializeField] private int upgradeRemoveAmount;
    public override void Apply(Combat_Handler combatHandler, int level)
    {
        combatHandler.RemoveBlock(isHostile, GetTotalStat(level));
    }

    public override int GetTotalStat(int level)
    {
        return removeAmount + upgradeRemoveAmount * Mathf.Max(0, level - 1);
    }
}
