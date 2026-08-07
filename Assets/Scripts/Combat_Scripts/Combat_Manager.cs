using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Combat_Manager : MonoBehaviour
{
    [SerializeField] private Combat_Handler combatHandler;
    [SerializeField] private Note_SO defaultNote;

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
        if (note.isHostile == true)
        {
            //TODO dodge to attack
            return;
        }
        else
        {
            note.Apply(combatHandler, note);
        }
    }

    private void OnNoteMiss()
    {
        if (Game_Manager.instance.isCombatActive == false) return;
        //Target player block
        if (combatHandler.Block(true)) return;

        combatHandler.DealDamage(defaultNote, 0);
    }

    private void OnNoteExit(Note_SO note)
    {
        if (note.isHostile == true)
        {
            note.Apply(combatHandler, note);
        }
    }
    #endregion
}
