using TMPro;
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
    [SerializeField] private UI_MP uiMP;

    [Header("Buttons")]
    [SerializeField] private Button buyButton;
    [SerializeField] private Button rerollButton;
    [SerializeField] private Button healButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        UpdateResources(0, 0);
    }

    public void ShowItemInfo(IBuy item)
    {
        itemName.text = item.Name + new string('+', item.Level);
        itemName.color = item.Color;
        if (item.Name == "")
        {
            int level = 1;
            foreach (var effect in Game_Manager.instance.statsManager.noteLevelTracker.GetUltimateEffect())
            {
                level--;
                level += Game_Manager.instance != null ? Game_Manager.instance.statsManager.noteLevelTracker.GetNoteLevel(effect) : 0;
            }

            itemName.text = "Ultimate" + new string('+', level);
            itemName.color = Color.white;
        }
        itemDescription.text = item.Description;
    }

    public void UpdateResources(int money, int hp)
    {
        uiMoney.UpdateMoney(money);
        uiHP.UpdateHP(Game_Manager.instance.statsManager.CurrentHP, Game_Manager.instance.statsManager.MaxHP, hp);
        uiMP.UpdateMP(Game_Manager.instance.statsManager.CurrentMP, Game_Manager.instance.statsManager.MaxMP, 0);
    }

    public void UpdateCost(int buyCost, int rerollCost, int healCost, int healAmount)
    {
        buyButton.GetComponentInChildren<TMP_Text>().text = $"Buy\n{buyCost}$";
        rerollButton.GetComponentInChildren<TMP_Text>().text = $"Reroll\n{rerollCost}$";
        healButton.GetComponentInChildren<TMP_Text>().text = $"Heal {healAmount}HP\n{healCost}$";
        exitButton.GetComponentInChildren<TMP_Text>().text = Game_Manager.instance.IsBossRound? "Boss Battle" : "To Battle";
    }

    public void ShowButton(bool canBuy, bool canReroll, bool canHeal)
    {
        buyButton.gameObject.SetActive(canBuy);
        rerollButton.gameObject.SetActive(canReroll);
        healButton.gameObject.SetActive(canHeal);
        exitButton.gameObject.SetActive(true);
    }
}
