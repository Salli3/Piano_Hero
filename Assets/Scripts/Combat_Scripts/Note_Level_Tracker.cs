using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Note_Level_Tracker : MonoBehaviour
{
    [SerializeField] private HashSet<Note_SO> playerAttackTypes = new HashSet<Note_SO>();
    private Dictionary<Note_SO, int> notesLevel = new Dictionary<Note_SO, int>();

    public void SetPlayerNote(Player_SO playerSO)
    {
        playerAttackTypes = playerSO.attackTypes.ToHashSet();
        notesLevel.Clear();
        foreach (var note in playerAttackTypes)
        {
            notesLevel[note] = 1;
        }
    }

    public void PurchaseNote(Note_SO note)
    {
        if (playerAttackTypes.Contains(note) == false) playerAttackTypes.Add(note);
        notesLevel[note] = GetNoteLevel(note) + 1;
    }

    public Note_SO[] GetNote() => playerAttackTypes.ToArray();

    public int GetNoteLevel(Note_SO note)
    {
        return notesLevel.TryGetValue(note, out int count) ? count : 0;
    }
}
