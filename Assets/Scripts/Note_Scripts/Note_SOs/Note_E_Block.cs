using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Block")]
public class Note_E_Block : Note_SO
{
    [SerializeField] private int blockTime;
    [SerializeField] private int upgradeBlockTime;

    private void Reset()
    {
        noteColor = Color.yellow;
    }

    public override void Apply(Combat_Manager combatManager)
    {
        combatManager.SetBlock(isHostile, GetTotalStat(Level));
    }

    public override int GetTotalStat(int level)
    {
        return blockTime + upgradeBlockTime * Mathf.Max(0, level - 1);
    }
}
