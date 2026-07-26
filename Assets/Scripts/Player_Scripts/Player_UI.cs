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
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        hpText.text = Mathf.CeilToInt(Game_Manager.instance.statsManager.currentHP) + "/" + Mathf.CeilToInt(Game_Manager.instance.statsManager.maxHP);
        hpBar.maxValue = Game_Manager.instance.statsManager.maxHP;
        hpBar.value = Game_Manager.instance.statsManager.currentHP;
    }

    public void HitRespond()
    {
        hpBarAnim.Play("HP_Decrease");
        cameraShake.Shake(duration, magnitude);
    }
}
