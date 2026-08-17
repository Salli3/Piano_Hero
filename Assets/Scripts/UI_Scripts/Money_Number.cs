using System.Collections;
using TMPro;
using UnityEngine;

public class Money_Number : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private float floatSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private Color plusColor = Color.green;
    [SerializeField] private Color minusColor = Color.red;

    private Coroutine activeRoutine;

    public void Init(int amount, Vector3 spawnPoint, float textSize)
    {
        moneyText.text = amount > 0 ? $"+{amount}$" : $"{amount}$";
        moneyText.color = amount > 0 ? plusColor : minusColor;
        moneyText.fontSize = textSize;
        transform.position = spawnPoint;

        if (activeRoutine != null) StopCoroutine(activeRoutine);
        activeRoutine = StartCoroutine(FloatAndFade());
    }

    private IEnumerator FloatAndFade()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Color startColor = moneyText.color;

        while (elapsed < lifeTime)
        {
            float time = elapsed / lifeTime;
            transform.position = startPos + Vector3.up * floatSpeed * time;
            moneyText.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, time));
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
