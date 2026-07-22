using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Note_Effect : ScriptableObject
{
    public abstract void Apply(Combat_Manager combatManager, Note_SO note);
}
