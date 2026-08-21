using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    [SerializeField] private Animator hpBarAnim;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image hpFillImage;
    [SerializeField] private float duration = 0.2f;

    private Coroutine hpRoutine;

    public void UpdateHP(int currentHP, int maxHP, int amount)
    {
        hpText.text = "HP: " + currentHP + "/" + maxHP;
        hpBar.maxValue = maxHP;

        if (amount != 0)
        {
            hpBarAnim.Play("HP_Update");
            if (hpRoutine != null) StopCoroutine(hpRoutine);
            hpRoutine = StartCoroutine(AnimateHP(currentHP));
        }
        else
        {
            if (hpRoutine != null) StopCoroutine(hpRoutine);
            hpFillImage.color = Color.white;
            hpBar.value = currentHP;
        }
    }

    private IEnumerator AnimateHP(int targetHP)
    {
        float startValue = hpBar.value;
        hpFillImage.color = startValue > targetHP ? Color.red : Color.green;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            hpBar.value = Mathf.Lerp(startValue, targetHP, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        hpFillImage.color = Color.white;
        hpBar.value = targetHP;
        hpRoutine = null;
    }
}