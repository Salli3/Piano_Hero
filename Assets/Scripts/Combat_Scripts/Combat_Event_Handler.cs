using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Combat_Event_Handler : MonoBehaviour
{
    [SerializeField] private Combat_Manager combatManager;

    #region Event subscribers
    private void OnEnable()
    {
        Judgement_Box.OnNoteHit += OnNoteHit;
        Judgement_Box.OnNoteMiss += OnNoteMiss;
        Note_Exit.OnNoteExit += OnNoteExit;
    }

    private void OnDisable()
    {
        Judgement_Box.OnNoteHit -= OnNoteHit;
        Judgement_Box.OnNoteMiss -= OnNoteMiss;
        Note_Exit.OnNoteExit -= OnNoteExit;
    }
    #endregion

    #region Combat methods
    private void OnNoteHit(Note_SO note)
    {
        if (note.isHostile == false)
        {
            note.Apply(combatManager);
        }
        combatManager.GainMP(note.isHostile, note.mpGain);
    }

    private void OnNoteMiss()
    {
        if (Game_Manager.instance.isCombatActive == false) return;
        combatManager.GainMP(true, 1);
        combatManager.DealDamageToPlayer();
    }

    private void OnNoteExit(Note_SO note)
    {
        if (note.isHostile == true)
        {
            note.Apply(combatManager);
            combatManager.GainMP(note.isHostile, note.mpGain);
        }
    }
    #endregion
}
