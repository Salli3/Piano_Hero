using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Note_Level_Tracker : MonoBehaviour
{
    [SerializeField] private List<Note_SO> playerAttackTypes = new List<Note_SO>();
    [SerializeField] private List<Note_SO> playerUltimateEffects = new List<Note_SO>();
    private Dictionary<Note_SO, int> notesLevel = new Dictionary<Note_SO, int>();

    private void Awake()
    {
        //For testing
        foreach (var note in playerAttackTypes)
        {
            notesLevel[note] = 1;
        }
    }

    public void SetPlayerNote(Player_SO playerSO)
    {
        playerAttackTypes = playerSO.attackTypes.ToList();
        playerUltimateEffects = playerSO.ultimate.UltimateEffects.ToList();
        notesLevel.Clear();
        foreach (var note in playerAttackTypes)
        {
            notesLevel[note] = 1;
        }
        foreach (var note in playerUltimateEffects)
        {
            notesLevel[note] = 1;
        }
    }

    public void PurchaseNote(Note_SO note)
    {
        if (playerAttackTypes.Contains(note) == false && note.Name != "") playerAttackTypes.Add(note);
        notesLevel[note] = GetNoteLevel(note) + 1;
    }

    public Note_SO[] GetNote() => playerAttackTypes.Distinct().ToArray();
    public Note_SO[] GetUltimateEffect() => playerUltimateEffects.Distinct().ToArray();

    public int GetNoteLevel(Note_SO note)
    {
        return notesLevel.TryGetValue(note, out int count) ? count : 0;
    }

    public void SetEnemyNote(Enemy_SO enemySO, int level)
    {
        foreach (var note in enemySO.attackTypes)
        {
            notesLevel[note] = level;
        }
    }
}
