using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Manager : MonoBehaviour
{
    [SerializeField] private Combat_Handler combatHandler;
    [SerializeField] private Player_HP playerHP;
    [SerializeField] private Enemy_HP enemyHP;
    [SerializeField] private Enemy_SO[] enemySOs;

    public Enemy_SO currentEnemy;

    #region Event subscribers
    private void OnEnable()
    {
        Enemy_HP.OnEnemyDefeated += PickEnemy;
        Judgement_Box.OnNoteHit += OnNoteHit;
        Judgement_Box.OnNoteMiss += OnNoteMiss;
        Note_Exit.OnNoteExit += OnNoteExit;
    }

    private void OnDisable()
    {
        Enemy_HP.OnEnemyDefeated -= PickEnemy;
        Judgement_Box.OnNoteHit -= OnNoteHit;
        Judgement_Box.OnNoteMiss -= OnNoteMiss;
        Note_Exit.OnNoteExit -= OnNoteExit;
    }
    #endregion

    private void Start()
    {
        PickEnemy();
    }

    private void PickEnemy()
    {
        currentEnemy = enemySOs[Random.Range(0, enemySOs.Length)];
        if (enemyHP != null)
        {
            enemyHP.SetEnemy(currentEnemy);
        }
    }

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
            if (combatHandler.Block(false)) return;
            note.Apply(combatHandler, note);
        }
    }

    private void OnNoteMiss()
    {
        if (Game_Manager.instance.isCombatActive == false) return;
        if (combatHandler.Block(true)) return;

        playerHP.ChangeHP(currentEnemy.enemyDamage);
    }

    private void OnNoteExit(Note_SO note)
    {
        if (note.isHostile == true)
        {
            if (combatHandler.Block(true)) return;
            note.Apply(combatHandler, note);
        }
    }
    #endregion
}
