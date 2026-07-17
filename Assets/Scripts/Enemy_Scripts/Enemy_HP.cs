using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_HP : MonoBehaviour
{
    [Header("Hit respond")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform enemyPosition;
    [SerializeField] private Image enemyImage;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    private Coroutine shakeRoutine;
    private Vector3 originalCameraPosition;
    private Vector3 originalEnemyPosition;

    #region Events subscriber
    private void OnEnable()
    {
        Combat_Events.OnNoteHit += NoteHit;
    }

    private void OnDisable()
    {
        Combat_Events.OnNoteHit -= NoteHit;
    }
    #endregion

    private void Start()
    {
        originalCameraPosition = mainCamera.transform.position;
        originalEnemyPosition = enemyPosition.position;
    }

    private void NoteHit(Note_SO note)
    {
        if (!note.isHostile)
        {
            ChangeHP(1);
        }
    }

    private void ChangeHP(float amount)
    {
        Shake();
    }

    #region Camera shake methods
    private void Shake()
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }
        shakeRoutine = StartCoroutine(DoShake());
    }

    private IEnumerator DoShake()
    {
        Time.timeScale = 0;
        float elapsed = 0f;
        enemyImage.color = Color.red;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.position = originalCameraPosition + new Vector3(x, y, 0f);
            enemyPosition.position = originalEnemyPosition + new Vector3(y * 100, x * 100, 0f);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalCameraPosition;
        enemyPosition.position = originalEnemyPosition;
        shakeRoutine = null;
        enemyImage.color = Color.white;
        Time.timeScale = 1;
    }
    #endregion
}
