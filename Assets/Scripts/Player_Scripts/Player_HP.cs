using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_HP : MonoBehaviour
{
    [SerializeField] private Player_UI playerUI;

    public void ChangeHP(int amount)
    {
        Game_Manager.instance.statsManager.currentHP -= amount;
        playerUI.UpdateUI();

        if (amount > 0)
        {
            playerUI.HitRespond();
            playerUI.ShowHitNumber(amount);

            if (Game_Manager.instance.statsManager.currentHP <= 0)
            {
                Game_Manager.instance.isCombatActive = false;
            }
        }
    }

    public void Block()
    {
        playerUI.ShowHitNumber(0, true);
    }

    public void UpdateCombatStatus(int block, int stackingDamage, int curse)
    {
        playerUI.UpdateCombatStatus(block, stackingDamage, curse);
    }

}
