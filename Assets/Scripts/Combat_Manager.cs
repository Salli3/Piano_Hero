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

    private void OnNoteHit(Note_SO note)
    {
        if (note.isHostile == true)
        {
            //TODO dodge to attack
            return;
        }
        else
        {
            StartCoroutine(AttackInterval(note));
        }
    }

    private IEnumerator AttackInterval(Note_SO note)
    {
        for (int i = 0; i < note.noteAttackTime; i++)
        {
            enemyHP.ChangeHP(note.noteDamage);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    private void OnNoteMiss()
    {
        playerHP.ChangeHP(1);
    }
    private void OnNoteExit(Note_SO note)
    {
        for (int i = 0; i < note.noteAttackTime; i++)
        {
            playerHP.ChangeHP(note.noteDamage);
        }
    }

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
