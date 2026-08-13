using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Stats_Manager : MonoBehaviour
{
    [SerializeField] private Player_SO player;
    public Note_Level_Tracker noteLevelTracker;
    public Passive_Level_Tracker passiveLevelTracker;

    [Header("Player Stats")]
    [SerializeField] private int damage;
    [SerializeField] private int currentHP;
    [SerializeField] private int maxHP;
    [SerializeField] private int money;

    //Public getter
    public Player_SO Player => player;
    public int Damage => damage;
    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;
    public int Money => money;

    private void Awake()
    {
        Initialize(player);
    }

    public void Initialize(Player_SO playerSO)
    {
        if (playerSO == null) return;
        player = playerSO;
        damage = playerSO.playerDamage;
        maxHP = playerSO.playerHP;
        currentHP = maxHP;
        money = playerSO.startingMoney;

        noteLevelTracker.SetPlayerNote(playerSO);
    }

    public void ModifyStat(Stat_Type type, int amount)
    {
        switch (type)
        {
            case Stat_Type.CurrentHP:
                currentHP += amount;
                if (currentHP > maxHP) currentHP = maxHP;
                break;
            case Stat_Type.MaxHP:
                maxHP += amount;
                currentHP += amount;
                break;
            case Stat_Type.Damage:
                damage += amount;
                break;
            case Stat_Type.Money:
                money += amount;
                break;
            default:
                Debug.LogWarning($"Stat not registered in ModifyStat: {type}");
                break;
        }
    }

    public int GetStat(Stat_Type type)
    {
        switch (type)
        {
            case Stat_Type.Damage: return damage;
            case Stat_Type.CurrentHP: return currentHP;
            case Stat_Type.MaxHP: return maxHP;
            case Stat_Type.Money: return money;
            default:
                return 0;
        }
    }
}