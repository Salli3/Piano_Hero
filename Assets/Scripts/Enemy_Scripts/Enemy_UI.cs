using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_UI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image enemyImage;
    [SerializeField] private Animator anim;

    [SerializeField] private CanvasGroup infoCG;
    [SerializeField] private CanvasGroup hpCG;
    [SerializeField] private CanvasGroup mpCG;

    [SerializeField] private UI_Status uiStatus;
    [SerializeField] private Hit_Number_Pool hitNumberPool;

    [SerializeField] private RectTransform enemyPosition; 
    private Vector3 originalEnemyPosition;

    [Header("Appear")]
    [SerializeField] private float appearDuration;
    [SerializeField] private float appearSlideDistance;

    [Header("Defeat")]
    [SerializeField] private float defeatShakeDuration;
    [SerializeField] private float defeatShakeMagnitude;
    [SerializeField] private float fallDistance;
    [SerializeField] private float slideDistance;

    private void OnEnable()
    {
        Combat_Manager.DamageEnemy += hitNumberPool.ShowHitNumber;
        Combat_Manager.EnemyStatusChange += uiStatus.UpdateCombatStatus;
    }

    private void OnDisable()
    {
        Combat_Manager.DamageEnemy -= hitNumberPool.ShowHitNumber;
        Combat_Manager.EnemyStatusChange -= uiStatus.UpdateCombatStatus;
    }

    private void Awake()
    {
        originalEnemyPosition = enemyPosition.position;
    }

    private void Start()
    {
        anim.Play("Start");
        infoCG.alpha = 0;
        hpCG.alpha = 0;
        mpCG.alpha = 0;
    }

    public void DisableAnimator() => anim.enabled = false;

    public void SetEnemyUI(Enemy_SO enemySO)
    {
        enemyImage.sprite = enemySO.enemySprite;
        nameText.text = enemySO.enemyName;

        infoCG.alpha = 1;
        hpCG.alpha = 1;
        mpCG.alpha = 1;
    }

    //TODO rework enemy appear animation
    #region Enemy Appear and Defeat animation methods
    public IEnumerator EnemyAppear()
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
    }

    public IEnumerator EnemyDefeat()
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
            float x = Random.Range(-1f, 1f) * defeatShakeMagnitude;
            float y = Random.Range(-1f, 1f) * defeatShakeMagnitude;
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
    }
    #endregion
}
