using UnityEngine;

[CreateAssetMenu(fileName = "UltimateSO")]
public class Ultimate_SO : ScriptableObject
{
    [SerializeField] private string ultName;
    [SerializeField, TextArea] private string ultDescription;
    [SerializeField] private int mpCost;
    [SerializeField] private Note_SO[] effects;

    public string Name => ultName;
    public string Description => ultDescription;
    public int MpCost => mpCost;

    public void Apply(Combat_Manager combatManager)
    {
        foreach (var effect in effects)
        {
            effect.Apply(combatManager);
        }
    }
}
