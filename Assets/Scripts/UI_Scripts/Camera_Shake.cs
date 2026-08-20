using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Camera_Shake : MonoBehaviour
{
    [SerializeField] private Transform lanesPosition;
    [SerializeField] private RectTransform bgPosition;
    [SerializeField] private RectTransform targetPosition;
    [SerializeField] private Image targetImage;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    private Coroutine shakeRoutine;
    private Vector3 originalLanesPosition;
    private Vector3 originalBgPosition;
    private Vector3 originalTargetPosition;

    private void Awake()
    {
        originalLanesPosition = lanesPosition.position;
        originalBgPosition = bgPosition.position;
        originalTargetPosition = targetPosition.position;
    }

    public void Shake()
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }
        shakeRoutine = StartCoroutine(DoShake());
    }

    private IEnumerator DoShake()
    {
        float elapsed = 0f;
        targetImage.color = Color.red;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            lanesPosition.position = originalLanesPosition + new Vector3(x / 100, y / 100, 0f);
            bgPosition.position = originalBgPosition + new Vector3(x, y, 0f);
            targetPosition.position = originalTargetPosition + new Vector3(y, x, 0f);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        lanesPosition.position = originalLanesPosition;
        bgPosition.position = originalBgPosition;
        targetPosition.position = originalTargetPosition;
        shakeRoutine = null;
        targetImage.color = Color.white;
    }
}