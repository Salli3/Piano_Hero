using TMPro;
using UnityEngine;

public class UI_Money : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private GameObject moneyNumberPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    private float textSize => moneyText.fontSize;

    private void OnEnable() => Enemy_HP.OnEnemyDefeated += OnEnemyDefeat;
    private void OnDisable() => Enemy_HP.OnEnemyDefeated -= OnEnemyDefeat;

    private void Start()
    {
        UpdateMoney();
    }

    private void OnEnemyDefeat(Enemy_SO enemySO)
    {
        UpdateMoney(enemySO.enemyMoneyReward);
    }

    public void UpdateMoney(int amount = 0)
    {
        if (amount != 0) ShowNumber(amount);

        moneyText.text = "Money: " + Game_Manager.instance.statsManager.Money + "$";
    }

    private void ShowNumber(int amount)
    {
        Vector3 baseOffset = new Vector3(xOffset, yOffset, 0);
        Money_Number money = Instantiate(moneyNumberPrefab, transform).GetComponent<Money_Number>();
        money.Init(amount, spawnPoint.position + baseOffset, textSize);
    }

    private void OnDrawGizmos()
    {
        if (spawnPoint == null) return;
        Gizmos.color = Color.yellow;
        Vector3 baseOffset = new Vector3(xOffset, yOffset, 0);
        Gizmos.DrawWireCube(spawnPoint.position + baseOffset, new Vector3(100, 100, 0));
    }
}
