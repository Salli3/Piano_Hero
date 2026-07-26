using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Manager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Shop_UI shopUI;
    [SerializeField] private Note_SO currentItem;
    [SerializeField] private Note_SO empty;
    [SerializeField] private Note_SO[] Items;

    [Header("Shop setting")]
    [SerializeField] private int buyCost;
    [SerializeField] private int rerollCost;
    [SerializeField] private int healCost;
    [SerializeField] private int inflation;
    [SerializeField] private float healAmount = 1f;

    private void Start()
    {
        GetNewItem();
    }

    private void GetNewItem()
    {
        if (Game_Manager.instance.statsManager.money >= buyCost)
        {
            currentItem = Items[Random.Range(0, Items.Length)];
            shopUI.ShowItemInfo(currentItem);
        }
        else
        {
            shopUI.ShowItemInfo(empty);
        }
        UpdateUI();
    }

    //Update resources and show available button
    private void UpdateUI()
    {
        shopUI.UpdateResources();
        shopUI.UpdateCost(buyCost,rerollCost, healCost);
        shopUI.ShowButton(
            canBuy: Game_Manager.instance.statsManager.money >= buyCost,
            canReroll: Game_Manager.instance.statsManager.money >= rerollCost + buyCost,
            canHeal: Game_Manager.instance.statsManager.money >= healCost
            && Game_Manager.instance.statsManager.maxHP > Game_Manager.instance.statsManager.currentHP
            );
    }

    #region Button press methods
    public void OnBuyButtonPressed()
    {
        Game_Manager.instance.statsManager.playerAttackTypes.Add(currentItem);
        Game_Manager.instance.statsManager.money -= buyCost;
        GetNewItem();
    }

    public void OnRerollButtonPressed()
    {
        Game_Manager.instance.statsManager.money -= rerollCost;
        rerollCost += inflation;
        GetNewItem();
    }

    public void OnHealButtonPressed()
    {
        Game_Manager.instance.statsManager.UpdateCurrentHP(healAmount);
        Game_Manager.instance.statsManager.money -= healCost;
        healCost += inflation;
        UpdateUI();
    }

    public void OnExitButtonPressed()
    {
        Game_Manager.instance.StartCombat();
    }
    #endregion
}