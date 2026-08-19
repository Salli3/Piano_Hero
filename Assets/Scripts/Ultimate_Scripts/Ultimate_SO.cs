using UnityEngine;

public abstract class Ultimate_SO : ScriptableObject
{
    [SerializeField] private string ultName;
    [SerializeField, TextArea] protected string ultDescription;
    public bool isHostile;

    public abstract void Apply(Combat_Manager combatManager);

}
