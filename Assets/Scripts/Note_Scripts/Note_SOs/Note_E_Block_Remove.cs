using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Block_Remove")]
public class Note_E_Block_Remove : Note_SO
{
    [SerializeField] private int removeAmount;
    [SerializeField] private int upgradeRemoveAmount;

    private void Reset()
    {
        noteColor = Color.white;
    }

    public override void Apply(Combat_Manager combatManager)
    {
        combatManager.RemoveBlock(isHostile, GetTotalStat(Level));
    }

    public override int GetTotalStat(int level)
    {
        return removeAmount + upgradeRemoveAmount * Mathf.Max(0, level - 1);
    }
}
