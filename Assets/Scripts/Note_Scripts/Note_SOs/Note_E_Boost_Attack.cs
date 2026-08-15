using UnityEngine;

[CreateAssetMenu(menuName = "Notes/Boost_Attack")]
public class Note_E_Boost_Attack : Note_SO
{
    [SerializeField] private int boostTime;
    [SerializeField] private int upgradeBoostTime;

    private void Reset()
    {
        noteColor = Color.blue;
    }

    public override void Apply(Combat_Manager combatManager)
    {
        combatManager.SetAttackBoost(isHostile, GetTotalStat(Level));
    }

    public override int GetTotalStat(int level)
    {
        return boostTime + upgradeBoostTime * Mathf.Max(0, level - 1);
    }
}
