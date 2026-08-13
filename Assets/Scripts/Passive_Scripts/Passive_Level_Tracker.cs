using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Passive_Level_Tracker : MonoBehaviour
{
    [SerializeField] private List<Passive_SO> playerPassives = new List<Passive_SO>();
    private Dictionary<Passive_SO, int> passivesLevel = new Dictionary<Passive_SO, int>();

    private void Awake()
    {
        foreach (var note in playerPassives)
        {
            passivesLevel[note] = 1;
        }
    }

    public void PurchasePassive(Passive_SO passive)
    {
        if (playerPassives.Contains(passive) == false) playerPassives.Add(passive);
        passivesLevel[passive] = GetPassiveLevel(passive) + 1;
    }

    public int GetPassiveLevel(Passive_SO passive)
    {
        return passivesLevel.TryGetValue(passive, out int count) ? count : 0;
    }
}
