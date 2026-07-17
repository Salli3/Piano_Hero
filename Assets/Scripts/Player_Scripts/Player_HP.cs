using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_HP : MonoBehaviour
{
    [Header("Hit respond")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform playerPosition;
    [SerializeField] private Image playerImage;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    private Coroutine shakeRoutine;
    private Vector3 originalCameraPosition;
    private Vector3 originalPlayerPosition;

    private void OnEnable()
    {
        Combat_Events.OnNoteMiss += NoteMiss;
        Combat_Events.OnNoteExit += NoteExit;
        
    }

    private void OnDisable()
    {
        Combat_Events.OnNoteMiss -= NoteMiss;
        Combat_Events.OnNoteExit -= NoteExit;
    }

    private void Start()
    {
        originalCameraPosition = mainCamera.transform.position;
        originalPlayerPosition = playerPosition.position;
    }

    private void NoteMiss()
    {
        ChangeHP(1);
    }

    private void NoteExit(Note_SO note)
    {
        if (note.isHostile)
        {
            ChangeHP(1);
        }
    }

    private void ChangeHP(float amount)
    {
        Shake();
    }

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
        float elapsed = 0f;
        playerImage.color = Color.red;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.position = originalCameraPosition + new Vector3(x, y, 0f);
            playerPosition.position = originalPlayerPosition + new Vector3(y*100, x*100, 0f);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalCameraPosition;
        playerPosition.position = originalPlayerPosition;
        shakeRoutine = null;
        playerImage.color = Color.white;
    }
}
