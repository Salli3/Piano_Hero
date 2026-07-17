using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combat_Events
{
    public static event Action<Note_SO> OnNoteExit;
    public static event Action<Note_SO> OnNoteHit;
    public static event Action OnNoteMiss;

    public static void NoteExit(Note_SO note) => OnNoteExit?.Invoke(note);
    public static void NoteHit(Note_SO note) => OnNoteHit?.Invoke(note);
    public static void NoteMiss() => OnNoteMiss?.Invoke();
}
