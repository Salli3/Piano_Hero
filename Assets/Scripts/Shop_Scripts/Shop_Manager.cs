using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private int healAmount = 1;

    private void Start()
    {
        Items = Items.Union(Game_Manager.instance.statsManager.GetNote()).ToArray();
        GetNewItem();
    }

    private void GetNewItem()
    {
        if (Game_Manager.instance.statsManager.Money >= buyCost)
        {
            List<Note_SO> availableItems = new List<Note_SO>(Items);
            if (currentItem != null)
            {
                availableItems.Remove(currentItem);
            }

            currentItem = availableItems[Random.Range(0, availableItems.Count)];
            shopUI.ShowItemInfo(currentItem);
        }
        UpdateUI();
    }

    //Update resources and show available button, hide item if not able to buy
    private void UpdateUI()
    {
        if (Game_Manager.instance.statsManager.Money < buyCost)
        {
            shopUI.ShowItemInfo(empty);
        }
        shopUI.UpdateResources();
        shopUI.UpdateCost(buyCost, rerollCost, healCost, healAmount);
        shopUI.ShowButton(
            canBuy: Game_Manager.instance.statsManager.Money >= buyCost,
            canReroll: Game_Manager.instance.statsManager.Money >= rerollCost + buyCost,
            canHeal: Game_Manager.instance.statsManager.Money >= healCost
            && Game_Manager.instance.statsManager.MaxHP > Game_Manager.instance.statsManager.CurrentHP
            );
    }

    #region Button press methods
    public void OnBuyButtonPressed()
    {
        Game_Manager.instance.statsManager.PurchaseNote(currentItem);
        Game_Manager.instance.statsManager.UpdateCurrentMoney(buyCost);
        shopUI.UpdateResources(buyCost);
        currentItem = null;
        GetNewItem();
    }

    public void OnRerollButtonPressed()
    {
        Game_Manager.instance.statsManager.UpdateCurrentMoney(rerollCost);
        shopUI.UpdateResources(rerollCost);
        rerollCost += inflation;
        GetNewItem();
    }

    public void OnHealButtonPressed()
    {
        Game_Manager.instance.statsManager.UpdateCurrentHP(-healAmount);
        Game_Manager.instance.statsManager.UpdateCurrentMoney(healCost);
        shopUI.UpdateResources(healCost, healAmount);
        healCost += inflation;
        UpdateUI();
    }

    public void OnExitButtonPressed()
    {
        Game_Manager.instance.StartCombatScene();
    }
    #endregion
}