using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stats_Manager : MonoBehaviour
{
    [Header("Player Stats")]
    public int damage;
    public int currentHP;
    public int maxHP;
    public int money;
    [SerializeField] private List<Note_SO> playerAttackTypes;

    private Dictionary<Note_SO, int> noteStackCounts = new Dictionary<Note_SO, int>();  

    private void Start()
    {
        foreach (var note in playerAttackTypes)
        {
            noteStackCounts[note] = 1;
        }
    }

    public void UpdateCurrentHP(int amount)
    {
        currentHP += amount;
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
    }

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
}
