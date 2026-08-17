using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hit_Number : MonoBehaviour
{
    [SerializeField] private TMP_Text hitText;
    [SerializeField] private Image hitImage;
    [SerializeField] private Sprite damageImage;
    [SerializeField] private Sprite blockImage;
    [SerializeField] private float floatSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color healColor = Color.green;
    [SerializeField] private Color blockedColor = Color.white;

    private Hit_Number_Pool hitNumberPool;
    private Coroutine activeRoutine;

    public void Show(int amount, Vector3 worldPosition, Hit_Number_Pool pool, bool isBlocked)
    {
        hitNumberPool = pool;
        transform.position = worldPosition;

        if (isBlocked)
        {
            hitText.text = "Block";
            hitImage.sprite = blockImage;
            hitImage.color = blockedColor;
        }
        else
        {
            hitText.text = amount > 0 ? $"+{amount}" : $"{amount}";
            hitImage.sprite = damageImage;
            hitImage.color = amount > 0 ? healColor : damageColor;
        }
        
        gameObject.SetActive(true);

        if (activeRoutine != null) StopCoroutine(activeRoutine);
        activeRoutine = StartCoroutine(FloatAndFade());
    }

    private IEnumerator FloatAndFade()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Color startColor = hitImage.color;

        while (elapsed < lifeTime)
        {
            float time = elapsed / lifeTime;
            transform.position = startPos + Vector3.up * floatSpeed * time;
            hitText.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, time));
            hitImage.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, time));
            elapsed += Time.deltaTime;
            yield return null;
        }

        hitNumberPool.ReturnToPool(this);
    }
}