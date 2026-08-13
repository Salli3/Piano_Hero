using UnityEngine;

[CreateAssetMenu(menuName = "Passives/Stat_Modifier")]
public class Passive_E_Stat_Modifier : Passive_SO
{
    [SerializeField] private Stat_Type type;
    [SerializeField] private int amount;

    public override void Apply()
    {
        Game_Manager.instance.statsManager.ModifyStat(type, amount);
    }

    public override int GetTotalStat()
    {
        return Game_Manager.instance.statsManager.GetStat(type) + amount;
    }
}
