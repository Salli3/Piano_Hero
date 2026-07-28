using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    [Header("Resources")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Slider hpBar;

    [Header("Buttons")]
    [SerializeField] private Button buyButton;
    [SerializeField] private Button rerollButton;
    [SerializeField] private Button healButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        UpdateResources();
    }

    public void ShowItemInfo(Note_SO note)
    {
        itemName.text = note.noteName + new string('+', Game_Manager.instance.statsManager.GetStackCount(note));
        itemDescription.text = note.GetDescription(Game_Manager.instance.statsManager.GetStackCount(note));
        Debug.Log($"note: {note.noteName}, owned: {Game_Manager.instance.statsManager.GetStackCount(note)}");
    }

    public void UpdateResources()
    {
        moneyText.text = "Money: " + Game_Manager.instance.statsManager.money + "$";
        hpText.text = Mathf.CeilToInt(Game_Manager.instance.statsManager.currentHP) + "/" + Mathf.CeilToInt(Game_Manager.instance.statsManager.maxHP);
        hpBar.maxValue = Game_Manager.instance.statsManager.maxHP;
        hpBar.value = Game_Manager.instance.statsManager.currentHP;
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
