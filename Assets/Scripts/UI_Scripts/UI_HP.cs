using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    [SerializeField] private Camera_Shake cameraShake;
    [SerializeField] private Animator hpBarAnim;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;

    public void UpdateHP(int currentHP, int maxHP, int amount)
    {
        if (amount < 0)
        {
            hpBarAnim.Play("HP_Decrease");
            if (cameraShake != null) cameraShake.Shake();
        }
        else if (amount > 0)
        {
            hpBarAnim.Play("HP_Increase");
        }
        hpText.text = "HP: " + currentHP + "/" + maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;
    }
}
