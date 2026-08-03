using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Stats_Manager : MonoBehaviour
{
    [SerializeField] private Player_SO player;

    [Header("Player Stats")]
    [SerializeField] private int damage;
    [SerializeField] private int currentHP;
    [SerializeField] private int maxHP;
    [SerializeField] private int money;

    public Player_SO Player => player;
    public int Damage => damage;
    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;
    public int Money => money;

    [SerializeField] private List<Note_SO> playerAttackTypes = new List<Note_SO>();
    private Dictionary<Note_SO, int> noteStackCounts = new Dictionary<Note_SO, int>();

    private void Start()
    {
        //For developer convenience only, delete this when build
        foreach (var note in playerAttackTypes)
        {
            noteStackCounts[note] = 1;
        }
    }

    public void Initialize(Player_SO playerSO)
    {
        player = playerSO;
        damage = playerSO.playerDamage;
        maxHP = playerSO.playerHP;
        currentHP = maxHP;
        money = playerSO.startingMoney;

        playerAttackTypes = playerSO.attackTypes.ToList();

        noteStackCounts.Clear();
        foreach (var note in playerAttackTypes)
        {
            noteStackCounts[note] = 1;
        }
    }

    public void UpdateCurrentHP(int amount)
    {
        currentHP -= amount;
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void UpdateCurrentMoney(int amount)
    {
        money -= amount;
    }

    #region Note level tracker
    public void PurchaseNote(Note_SO note)
    {
        if (playerAttackTypes.Contains(note) == false) playerAttackTypes.Add(note);

        noteStackCounts[note] = GetStackCount(note) + 1;
    }

    public Note_SO[] GetNote()
    {
        return playerAttackTypes.ToArray();
    }

    public int GetStackCount(Note_SO note)
    {
        return noteStackCounts.TryGetValue(note, out int count) ? count : 0;
    }
    #endregion
}
