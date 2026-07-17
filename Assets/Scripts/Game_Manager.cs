using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    [SerializeField] private float difficultylevel = 1f;
    [SerializeField] private float difficultyMultiplier = 1.2f;

    public bool isCombatActive;
    public Enemy_SO currentEnemy;
    public Player_Combat playerCombat;
    public Note_SO[] currentNotes
    => currentEnemy.attackTypes.Concat(playerCombat.attackTypes).ToArray();

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    #region Persistent data marking and Singleton methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
        else
        {
            CleanUpAndDestroy();
            return;
        }
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        Destroy(gameObject);
    }
    #endregion

    public float GetDifficultyLevel()
    {
        return difficultylevel;
    }
}
