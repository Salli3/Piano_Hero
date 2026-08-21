using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MP : MonoBehaviour
{
    [SerializeField] private Animator mpBarAnim;
    [SerializeField] private Slider mpBar;
    [SerializeField] private TMP_Text mpText;
    [SerializeField] private Image mpFillImage;
    [SerializeField] private float duration = 0.2f;

    private Coroutine mpRoutine;

    public void UpdateMP(int currentMP, int maxMP, int amount)
    {
        mpText.text = "MP: " + currentMP + "/" + maxMP;
        mpBar.maxValue = maxMP;  

        if (amount != 0)
        {
            mpBarAnim.Play("HP_Update");
            if (mpRoutine != null) StopCoroutine(mpRoutine);
            mpRoutine = StartCoroutine(AnimateMP(currentMP));
        }
        else
        {
            if (mpRoutine != null) StopCoroutine(mpRoutine);
            mpFillImage.color = Color.white;
            mpBar.value = currentMP;
        }
    }

    private IEnumerator AnimateMP(int targetMP)
    {
        float startValue = mpBar.value;
        mpFillImage.color = startValue > targetMP ? Color.magenta : Color.cyan; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            mpBar.value = Mathf.Lerp(startValue, targetMP, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        mpFillImage.color = Color.white;
        mpBar.value = targetMP;
        mpRoutine = null;
    }
}