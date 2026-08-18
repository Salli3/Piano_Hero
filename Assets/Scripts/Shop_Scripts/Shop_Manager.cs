using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop_Manager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Shop_UI shopUI;

    [Header("Item Pools")]
    [SerializeField] private Note_SO[] noteItems;
    [SerializeField] private Passive_SO[] passiveItems;
    [SerializeField] private Note_SO empty;

    [Header("Shop setting")]
    [SerializeField] private int buyCost;
    [SerializeField] private int rerollCost;
    [SerializeField] private int healCost;
    [SerializeField] private int inflation;
    [SerializeField] private int healAmount = 1;

    private List<IBuy> allItems = new();
    private IBuy currentItem;

    private void Start()
    {
        allItems.AddRange(noteItems);
        allItems.AddRange(passiveItems);
        allItems = allItems.Union(Game_Manager.instance.statsManager.noteLevelTracker.GetNote()).ToList();

        GetNewItem();
    }

    private void GetNewItem()
    {
        if (Game_Manager.instance.statsManager.Money >= buyCost)
        {
            List<IBuy> availableItems = new List<IBuy>(allItems.Distinct());
            if (currentItem != null)
            {
                availableItems.Remove(currentItem); // prevent duplicate pick on reroll
            }

            currentItem = availableItems[Random.Range(0, availableItems.Count)];
            shopUI.ShowItemInfo(currentItem);
            //Debug.Log($"Showing {currentItem}");
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
        currentItem.BuyItem();
        Game_Manager.instance.statsManager.ModifyStat(Stat_Type.Money, -buyCost);
        shopUI.UpdateResources(-buyCost);
        currentItem = null;
        GetNewItem();
    }

    public void OnRerollButtonPressed()
    {
        Game_Manager.instance.statsManager.ModifyStat(Stat_Type.Money, -rerollCost);
        shopUI.UpdateResources(-rerollCost);
        rerollCost += inflation;
        GetNewItem();
    }

    public void OnHealButtonPressed()
    {
        Game_Manager.instance.statsManager.ModifyStat(Stat_Type.CurrentHP, healAmount);
        Game_Manager.instance.statsManager.ModifyStat(Stat_Type.Money, -healCost);
        shopUI.UpdateResources(-healCost, healAmount);
        healCost += inflation;
        UpdateUI();
    }

    public void OnExitButtonPressed()
    {
        Game_Manager.instance.fadeUI.FadeOut();
    }
    #endregion
}