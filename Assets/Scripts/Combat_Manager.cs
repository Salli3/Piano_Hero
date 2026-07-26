using System.Collections;
using System.Linq;
using UnityEngine;

public class Combat_Manager : MonoBehaviour
{
    [SerializeField] private Note_Effect_Handler noteEffectHandler;
    [SerializeField] private Player_HP playerHP;
    [SerializeField] private Enemy_HP enemyHP;
    [SerializeField] private Enemy_SO currentEnemy;
    [SerializeField] private Enemy_SO[] enemySOs;

    public Note_SO[] currentNotes
    => Game_Manager.instance.statsManager.playerAttackTypes.Concat(currentEnemy.attackTypes).ToArray();

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
            note.Apply(noteEffectHandler, note);
        }
    }

    private void OnNoteMiss()
    {
        if (Game_Manager.instance.isCombatActive == false) return;
        if (noteEffectHandler.Block()) return;

        playerHP.ChangeHP(1);
    }

    private void OnNoteExit(Note_SO note)
    {
        if (note.isHostile == true)
        {
            if (noteEffectHandler.Block()) return;

            note.Apply(noteEffectHandler, note);
        }
    }
    #endregion
}
