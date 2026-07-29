using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Effect_Handler : MonoBehaviour
{
    [SerializeField] private Player_HP playerHP;
    [SerializeField] private Enemy_HP enemyHP;

    [SerializeField] private int block;
    [SerializeField] private int stackingDamage;
    [SerializeField] private int enemyStackingDamage;

    //Deal Damage
    public void DealDamage(Note_SO note, int damage)
    {
        if (note.isHostile)
        {
            DamagePlayer(damage);
        }
        else
        {
            DamageEnemy(damage);
        }
    }
    private void DamagePlayer(int amount) => playerHP.ChangeHP(amount);
    private void DamageEnemy(int amount) => enemyHP.ChangeHP(amount + Game_Manager.instance.statsManager.damage);

    //Block
    public void SetBlock(int amount) => block = amount;
    public bool Block()
    {
        if (block > 0)
        {
            block--;
            playerHP.Block();
            return true;
        }
        else
        {
            return false;
        }
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
    public int StackDamage(int amount, Note_SO note) 
    {
        if (note.isHostile)
        {
            return enemyStackingDamage += amount;
        }
        else
        {
            return stackingDamage += amount;
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
