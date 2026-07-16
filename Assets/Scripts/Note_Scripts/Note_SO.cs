using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoteSO")]
public class Note_SO : ScriptableObject
{
    public string noteName;
    public float noteSpeed;
    public bool isHostile;
    public Color noteColor => isHostile ? Color.red : Color.blue;
}
