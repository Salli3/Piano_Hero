using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_HP : MonoBehaviour
{
    [Header("Hit respond")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform enemyPosition;
    [SerializeField] private Image enemyImage;
    [SerializeField] private Animator hpBarAnim;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    private Coroutine shakeRoutine;
    private Vector3 originalCameraPosition;
    private Vector3 originalEnemyPosition;

    [Header("Enemy HP")]
    public Enemy_SO enemySO;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Slider hpBar;
    [SerializeField] private float currentHP;
    public static event Action OnEnemyDefeated;

    [Header("Appear")]
    [SerializeField] private float appearDuration;
    [SerializeField] private float appearSlideDistance;

    [Header("Defeat")]
    [SerializeField] private float defeatShakeDuration;
    [SerializeField] private float defeatShakeMagnitude;
    [SerializeField] private float fallDistance;
    [SerializeField] private float slideDistance;

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

    private void Awake()
    {
        originalCameraPosition = mainCamera.transform.position;
        originalEnemyPosition = enemyPosition.position;
    }

    public void SetEnemy(Enemy_SO newEnemy)
    {
        enemySO = Game_Manager.instance.currentEnemy;
        currentHP = enemySO.enemyHP;
        enemyImage.sprite = enemySO.enemySprite;
        StartCoroutine(EnemyAppear());
        UpdateUI();
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
        currentHP -= amount;
        Shake();
        UpdateUI();
        hpBarAnim.Play("HP_Decrease");
        if (currentHP <= 0)
        {
            Game_Manager.instance.isCombatActive = false;
            StartCoroutine(EnemyDefeat());
        }
    }

    private void UpdateUI()
    {
        hpText.text = Mathf.CeilToInt(currentHP) + "/" + Mathf.CeilToInt(enemySO.enemyHP);
        hpBar.maxValue = enemySO.enemyHP;
        hpBar.value = currentHP;
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
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

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

    //TODO rework enemy appear animation
    #region Enemy Appear and Defeat animation methods
    private IEnumerator EnemyAppear()
    {
        float elapsed = 0f;
        Color startColor = Color.white;
        Vector3 startOffset = new Vector3(appearSlideDistance, 0f, 0f);

        enemyPosition.position = originalEnemyPosition + startOffset;
        enemyImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsed < appearDuration)
        {
            float t = elapsed / appearDuration;

            enemyPosition.position = Vector3.Lerp(originalEnemyPosition + startOffset, originalEnemyPosition, t);

            //Fade in
            float alpha = Mathf.Lerp(0f, 1f, t);
            enemyImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        enemyPosition.position = originalEnemyPosition;
        enemyImage.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        Game_Manager.instance.isCombatActive = true;
    }

    private IEnumerator EnemyDefeat()
    {
        float elapsed = 0f;
        Color startColor = Color.white;
        Vector3 targetOffset = new Vector3(slideDistance, -fallDistance, 0f);

        while (elapsed < defeatShakeDuration)
        {
            float t = elapsed / defeatShakeDuration;

            //Slide
            Vector3 fallPos = originalEnemyPosition + targetOffset * t;

            //Shake
            float x = UnityEngine.Random.Range(-1f, 1f) * defeatShakeMagnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * defeatShakeMagnitude;
            Vector3 shakeOffset = new Vector3(x * 100, y * 100, 0f);

            enemyPosition.position = fallPos + shakeOffset;

            //Fade out
            float alpha = Mathf.Lerp(1f, 0f, t);
            enemyImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        enemyPosition.position = originalEnemyPosition;
        enemyImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        OnEnemyDefeated?.Invoke();
    }
    #endregion
}
