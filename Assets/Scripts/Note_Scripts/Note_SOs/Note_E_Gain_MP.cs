using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Gain_MP")]
public class Note_E_Gain_MP : Note_SO
{
    [SerializeField] private int mpExGain;
    [SerializeField] private int upgradeMpExGain;

    private void Reset()
    {
        noteColor = Color.cyan;
    }

    public override void Apply(Combat_Manager combatManager)
    {
        combatManager.GainMP(isHostile, GetTotalStat(Level));
    }

    public override int GetTotalStat(int level)
    {
        return mpExGain + upgradeMpExGain * Mathf.Max(0, level - 1);
    }
}
