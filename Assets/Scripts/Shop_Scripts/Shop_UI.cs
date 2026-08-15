using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    [Header("Resources")]
    [SerializeField] private UI_Money uiMoney;
    [SerializeField] private UI_HP uiHP;

    [Header("Buttons")]
    [SerializeField] private Button buyButton;
    [SerializeField] private Button rerollButton;
    [SerializeField] private Button healButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        UpdateResources();
    }

    public void ShowItemInfo(IBuy item)
    {
        itemName.text = item.Name + new string('+', item.Level);
        itemName.color = item.Color;
        itemDescription.text = item.Description;
    }

    public void UpdateResources(int money = 0, int hp = 0)
    {
        uiMoney.UpdateMoney(money);
        uiHP.UpdateHP(Game_Manager.instance.statsManager.CurrentHP, Game_Manager.instance.statsManager.MaxHP, hp);
    }

    public void UpdateCost(int buyCost, int rerollCost, int healCost, int healAmount)
    {
        buyButton.GetComponentInChildren<TMP_Text>().text = $"Buy\n{buyCost}$";
        rerollButton.GetComponentInChildren<TMP_Text>().text = $"Reroll\n{rerollCost}$";
        healButton.GetComponentInChildren<TMP_Text>().text = $"Heal {healAmount}HP\n{healCost}$";
    }

    public void ShowButton(bool canBuy, bool canReroll, bool canHeal)
    {
        buyButton.gameObject.SetActive(canBuy);
        rerollButton.gameObject.SetActive(canReroll);
        healButton.gameObject.SetActive(canHeal);
        exitButton.gameObject.SetActive(true);
    }
}
