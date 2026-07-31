using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_HP : MonoBehaviour, IHealth
{
    [SerializeField] private Player_UI playerUI;

    public void ChangeHP(int amount)
    {
        Game_Manager.instance.statsManager.UpdateCurrentHP(amount);

        if (amount != 0)
        {
            playerUI.UpdateHPUI(amount);
            playerUI.ShowHitNumber(amount);

            if (Game_Manager.instance.statsManager.CurrentHP <= 0)
            {
                Game_Manager.instance.isCombatActive = false;
            }
        }
    }
}
