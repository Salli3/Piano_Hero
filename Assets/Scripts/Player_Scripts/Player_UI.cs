using System.Collections;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera_Shake cameraShake;

    [Header("HP bar")]
    [SerializeField] private Animator hpBarAnim;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;

    [Header("Money")]
    [SerializeField] private TMP_Text moneyText;

    [Header("Hit number")]
    [SerializeField] private Hit_Number_Pool hitNumberPool;
    [SerializeField] private Transform hitNumberPosition;
    [SerializeField] private float hitNumberPositionOffset;
    [SerializeField] private float hitNumberAppearWidth;
    [SerializeField] private float hitNumberAppearHeigh;

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
        hpText.text = "HP: " + Game_Manager.instance.statsManager.currentHP + "/" + Game_Manager.instance.statsManager.maxHP;
        hpBar.maxValue = Game_Manager.instance.statsManager.maxHP;
        hpBar.value = Game_Manager.instance.statsManager.currentHP;
    }

    public void HitRespond()
    {
        hpBarAnim.Play("HP_Decrease");
        cameraShake.Shake();
    }

    public void ShowHitNumber(int damage, bool isBlocked = false)
    {
        float randomWidth = Random.Range(-hitNumberAppearWidth * 0.5f, hitNumberAppearWidth * 0.5f);
        float randomHeigh = Random.Range(-hitNumberAppearHeigh * 0.5f, hitNumberAppearHeigh * 0.5f);

        Vector3 randomOffset = new Vector3(randomWidth, randomHeigh, 0);
        Vector3 baseOffset = new Vector3(hitNumberPositionOffset, 0, 0);

        Vector3 spawnPosition = hitNumberPosition.position + baseOffset + randomOffset;

        hitNumberPool.GetHitNumber().ShowHitNumber(damage, spawnPosition, hitNumberPool, isBlocked);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 baseOffset = new Vector3(hitNumberPositionOffset, 0, 0);
        Gizmos.DrawWireCube(hitNumberPosition.position + baseOffset, new Vector3(hitNumberAppearWidth, hitNumberAppearHeigh, 0));
    }
}
