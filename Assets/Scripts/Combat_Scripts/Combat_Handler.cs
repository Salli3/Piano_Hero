using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Combat_Handler : MonoBehaviour
{
    [SerializeField] private Player_HP playerHP;
    [SerializeField] private Player_UI playerUI;
    [SerializeField] private Enemy_HP enemyHP;
    [SerializeField] private Enemy_UI enemyUI;

    [SerializeField] private int playerBlock;
    [SerializeField] private int playerStackingDamage;

    [SerializeField] private int enemyBlock;
    [SerializeField] private int enemyStackingDamage;

    private void Start()
    {
        UpdateCombatStatus();
    }

    private void UpdateCombatStatus()
    {
        playerUI.UpdateCombatStatus(playerBlock, playerStackingDamage);
        enemyUI.UpdateCombatStatus(enemyBlock, enemyStackingDamage);
    }

    //Deal Damage
    public void DealDamage(Note_SO note, int damage)
    {
        if (note.isHostile)
        {
            playerHP.ChangeHP(damage);
        }
        else
        {
            enemyHP.ChangeHP(damage + Game_Manager.instance.statsManager.damage);
        }
    }

    //Block
    public void SetBlock(Note_SO note, int amount)
    {
        if (note.isHostile)
        {
            enemyBlock = amount;
        }
        else
        {
            playerBlock = amount;
        }
        UpdateCombatStatus();
    }
    public bool Block(bool isHostile)
    {
        int currentBlock = isHostile ? playerBlock : enemyBlock;

        if (currentBlock <= 0) return false;

        if (isHostile)
        {
            playerBlock--;
            playerUI.ShowHitNumber(0, true);
        }
        else
        {
            enemyBlock--;
            enemyUI.ShowHitNumber(0, true);
        }
        UpdateCombatStatus();
        return true;
    }

    //Clear note
    public int ClearNote()
    {
        Note[] allNotes = FindObjectsByType<Note>(FindObjectsSortMode.None);
        int noteLayer = LayerMask.NameToLayer("Note");
        int clearedCount = 0;

        foreach (Note note in allNotes)
        {
            if (note.gameObject.layer == noteLayer && note.noteSO.isHostile)
            {
                clearedCount++;
                note.OnNoteHit();
            }
        }
        return clearedCount;
    }

    //Stack damage
    public int StackDamage(Note_SO note, int amount)
    {
        if (note.isHostile)
        {
            enemyStackingDamage += amount;
            UpdateCombatStatus();
            return enemyStackingDamage;
        }
        else
        {
            playerStackingDamage += amount;
            UpdateCombatStatus();
            return playerStackingDamage;
        }
    }

    //Multi hit
    public void RunMultiHit(Note_SO note, int damage, int hitTime)
    {
        StartCoroutine(AttackInterval(note, damage, hitTime));
    }
    private IEnumerator AttackInterval(Note_SO note, int damage, int hitTime)
    {
        for (int i = 0; i < hitTime; i++)
        {
            DealDamage(note, damage);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
