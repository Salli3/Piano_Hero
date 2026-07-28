using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [SerializeField] private Camera_Shake cameraShake;
    [SerializeField] private Animator hpBarAnim;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text moneyText;

    private void OnEnable()
    {
        Enemy_HP.OnEnemyDefeated += UpdateUI;
    }

    private void OnDisable()
    {
        Enemy_HP.OnEnemyDefeated -= UpdateUI;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyText.text = "Money: " + Game_Manager.instance.statsManager.money + "$";
        hpText.text = Game_Manager.instance.statsManager.currentHP + "/" + Game_Manager.instance.statsManager.maxHP;
        hpBar.maxValue = Game_Manager.instance.statsManager.maxHP;
        hpBar.value = Game_Manager.instance.statsManager.currentHP;
    }

    public void HitRespond()
    {
        hpBarAnim.Play("HP_Decrease");
        cameraShake.Shake();
    }
}
