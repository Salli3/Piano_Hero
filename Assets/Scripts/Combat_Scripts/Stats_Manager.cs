using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stats_Manager : MonoBehaviour
{
    [SerializeField] private Player_SO player;
    public Player_SO Player => player;

    private Dictionary<Stat_Type, int> stats = new();

    //Public getter
    public int Damage => GetStat(Stat_Type.Damage);
    public int CurrentHP => GetStat(Stat_Type.CurrentHP);
    public int MaxHP => GetStat(Stat_Type.MaxHP);
    public int Money => GetStat(Stat_Type.Money);

    public Note_Level_Tracker noteLevelTracker;

    private void Awake()
    {
        Initialize(player);
    }

    public void Initialize(Player_SO playerSO)
    {
        if (playerSO == null) return;
        player = playerSO;
        stats.Clear();

        stats[Stat_Type.Damage] = playerSO.playerDamage;
        stats[Stat_Type.MaxHP] = playerSO.playerHP;
        stats[Stat_Type.CurrentHP] = playerSO.playerHP;
        stats[Stat_Type.Money] = playerSO.startingMoney;

        noteLevelTracker.SetPlayerNote(playerSO);
    }

    public int GetStat(Stat_Type type)
    {
        return stats.TryGetValue(type, out int value) ? value : 0;
    }

    public void ModifyStat(Stat_Type type, int amount)
    {
        switch (type)
        {
            case Stat_Type.CurrentHP:
                stats[type] += amount;
                if (stats[type] > GetStat(Stat_Type.MaxHP)) stats[type] = GetStat(Stat_Type.MaxHP);
                break;
            case Stat_Type.MaxHP:
                stats[type] += amount;
                stats[Stat_Type.CurrentHP] += amount;
                break;
            default:
                stats[type] += amount;
                break;
        }
    }
}