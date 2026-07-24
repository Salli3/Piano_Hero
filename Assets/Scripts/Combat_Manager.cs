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
    [SerializeField] private Note_SO[] playerAttackTypes;

    [Header("Enemy info")]
    [SerializeField] private Enemy_HP enemyHP;
    [SerializeField] private Enemy_SO currentEnemy;
    [SerializeField] private Enemy_SO[] enemySOs;

    [Header("Combat info")]
    public bool isCombatActive;
    public Note_SO[] currentNotes
    => playerAttackTypes.Concat(currentEnemy.attackTypes).ToArray();

    [SerializeField] private float damage;
    [SerializeField] private int block;
    [SerializeField] private int stackingDamage;

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

    public void PickEnemy()
    {
        stackingDamage = 0; //Reset stacking damage when pick new enemy
        currentEnemy = enemySOs[Random.Range(0, enemySOs.Length)];
        if (enemyHP != null)
        {
            enemyHP.SetEnemy(currentEnemy);
            IncreaseDifficultyLevel();
        }
    }

    #region Note effect helper methods
    public void ApplyAttack(int num) => damage = num;
    public void ApplyBlockAttack(int num) => block = num;
    public void ApplyNoteClear(int num)
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
    public void ApplyStackingDamage(int num)
    {
        stackingDamage += num;
        damage = stackingDamage;
    }
    public void ApplyMultiHit(int damage, int hitTime)
    {
        this.damage = 0;
        StartCoroutine(AttackInterval(damage, hitTime));
    }
    private IEnumerator AttackInterval(int damage, int hitTime)
    {
        for (int i = 0; i < hitTime; i++)
        {
            enemyHP.ChangeHP(damage);
            yield return new WaitForSecondsRealtime(0.1f);
        }
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
            note.Apply(this, note);
            enemyHP.ChangeHP(damage);
        }
    }

    private void OnNoteMiss()
    {
        if (isCombatActive == false) return;
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
            playerHP.ChangeHP(1);
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
