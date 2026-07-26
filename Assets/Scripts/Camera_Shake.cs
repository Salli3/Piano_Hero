using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Camera_Shake : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform targetPosition;
    [SerializeField] private Image targetImage;
    [SerializeField] private bool freezeTimeScale;

    private Coroutine shakeRoutine;
    private Vector3 originalCameraPosition;
    private Vector3 originalTargetPosition;

    private void Awake()
    {
        originalCameraPosition = mainCamera.transform.position;
        originalTargetPosition = targetPosition.position;
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }
        shakeRoutine = StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        if (freezeTimeScale) Time.timeScale = 0;

        float elapsed = 0f;
        targetImage.color = Color.red;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.position = originalCameraPosition + new Vector3(x, y, 0f);
            targetPosition.position = originalTargetPosition + new Vector3(y * 100, x * 100, 0f);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalCameraPosition;
        targetPosition.position = originalTargetPosition;
        shakeRoutine = null;
        targetImage.color = Color.white;

        if (freezeTimeScale) Time.timeScale = 1;
    }
}