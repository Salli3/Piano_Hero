using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Combat_Manager : MonoBehaviour
{
    [Header("Difficulty settings")]
    [SerializeField] private float difficultylevel = 1f;
    [SerializeField] private float difficultyMultiplier = 1.2f;

    [Header("Player info")]
    [SerializeField] private Player_HP playerHP;
    public Note_SO[] playerAttackTypes;

    [Header("Enemy info")]
    [SerializeField] private Enemy_SO[] enemySOs;
    [SerializeField] private Enemy_HP enemyHP;
    public Enemy_SO currentEnemy;

    [Header("Combat info")]
    public bool isCombatActive;
    public Note_SO[] currentNotes
    => playerAttackTypes.Concat(currentEnemy.attackTypes).ToArray();

    [Header("Effect info")]
    [SerializeField] private float damage;
    [SerializeField] private int block;
    [SerializeField] private int stackingDamage;

    #region Event subscribers
    private void OnEnable()
    {
        Enemy_HP.OnEnemyDefeated += PickEnemy;
        Combat_Events.OnNoteHit += OnNoteHit;
        Combat_Events.OnNoteMiss += OnNoteMiss;
        Combat_Events.OnNoteExit += OnNoteExit;
    }

    private void OnDisable()
    {
        Enemy_HP.OnEnemyDefeated -= PickEnemy;
        Combat_Events.OnNoteHit -= OnNoteHit;
        Combat_Events.OnNoteMiss -= OnNoteMiss;
        Combat_Events.OnNoteExit -= OnNoteExit;
    }
    #endregion

    private void Start()
    {
        PickEnemy();
    }

    public void PickEnemy()
    {
        Game_Manager.instance.combatManager.currentEnemy = enemySOs[Random.Range(0, enemySOs.Length)];
        if (enemyHP != null)
        {
            enemyHP.SetEnemy(currentEnemy);
            IncreaseDifficultyLevel();
        }
    }

    #region Note effect helper methods
    public void SetBlockAttack(int num) => block = num;
    public void SetStackingDamage(int num)
    {
        stackingDamage += num;
        damage = stackingDamage;
    }
    public void SetNoteClear(int num)
    {
        Note[] allNotes = FindObjectsByType<Note>(FindObjectsSortMode.None);
        int noteLayer = LayerMask.NameToLayer("Note");

        int clearedCount = 0;

        foreach (Note note in allNotes)
        {
            if (note.gameObject.layer == noteLayer && note.noteSO.isHostile)
            {
                clearedCount++;
                Destroy(note.gameObject);
            }
        }

        damage = clearedCount * num;
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
            damage = note.noteDamage;
            note.noteEffect?.Apply(this, note);

            StartCoroutine(AttackInterval(note));
        }
    }

    private IEnumerator AttackInterval(Note_SO note)
    {
        for (int i = 0; i < note.noteAttackTime; i++)
        {
            enemyHP.ChangeHP(damage);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    private void OnNoteMiss()
    {
        if (block > 0)
        {
            block--;
            return;
        }

        playerHP.ChangeHP(1);
    }

    private void OnNoteExit(Note_SO note)
    {
        if (block > 0)
        {
            block--;
            return;
        }

        if (note.isHostile == true)
        {
            for (int i = 0; i < note.noteAttackTime; i++)
            {
                playerHP.ChangeHP(note.noteDamage);
            }
        }
    }
    #endregion

    #region Difficulty level setter/getter
    public float GetDifficultyLevel()
    {
        return difficultylevel;
    }

    private void IncreaseDifficultyLevel()
    {
        difficultylevel *= difficultyMultiplier;
    }
    #endregion
}
