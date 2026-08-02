using System.Collections;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour, IHitNumber
{
    [SerializeField] private UI_HP uiHP;
    [SerializeField] private UI_Money uiMoney;
    [SerializeField] private UI_Status uiStatus;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Hit_Number_Pool hitNumberPool;

    private void OnEnable()
    {
        Enemy_HP.OnEnemyDefeated += UpdateMoneyUI;
    }

    private void OnDisable()
    {
        Enemy_HP.OnEnemyDefeated -= UpdateMoneyUI;
    }

    private void Start()
    {
        UpdateHPUI();
        uiMoney.UpdateMoney();
    }

    private void UpdateMoneyUI(Enemy_SO enemySO) => uiMoney.UpdateMoney(-enemySO.enemyMoneyReward);

    public void UpdateHPUI(int amount = 0) => uiHP.UpdateHP(Game_Manager.instance.statsManager.CurrentHP, Game_Manager.instance.statsManager.MaxHP, amount);  

    public void ShowHitNumber(int damage, bool isBlocked = false) => hitNumberPool.ShowHitNumber(damage, isBlocked);

    public void UpdateCombatStatusUI(int block, int stackingDamage) => uiStatus.UpdateCombatStatus(block, stackingDamage);
}
