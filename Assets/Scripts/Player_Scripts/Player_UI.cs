using System.Collections;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [SerializeField] private UI_HP uiHP;
    [SerializeField] private UI_Status uiStatus;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Hit_Number_Pool hitNumberPool;

    private void OnEnable()
    {
        Enemy_HP.OnEnemyDefeated += UpdateMoney;
    }

    private void OnDisable()
    {
        Enemy_HP.OnEnemyDefeated -= UpdateMoney;
    }

    private void Start()
    {
        UpdateHPBar();
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        moneyText.text = "Money: " + Game_Manager.instance.statsManager.money + "$";
    } 

    public void UpdateHPBar(int amount = 0) => uiHP.UpdateHP(Game_Manager.instance.statsManager.currentHP, Game_Manager.instance.statsManager.maxHP, amount);  

    public void ShowHitNumber(int damage, bool isBlocked = false) => hitNumberPool.ShowHitNumber(damage, isBlocked);

    public void UpdateCombatStatus(int block, int stackingDamage) => uiStatus.UpdateCombatStatus(block, stackingDamage);
}
