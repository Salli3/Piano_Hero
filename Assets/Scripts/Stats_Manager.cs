using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats_Manager : MonoBehaviour
{
    [Header("Player Stats")]
    public float damage;
    public float currentHP;
    public float maxHP;
    public int money;
    public List<Note_SO> playerAttackTypes;

    public void UpdateCurrentHP(float amount)
    {
        currentHP += amount;
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
    }
}
