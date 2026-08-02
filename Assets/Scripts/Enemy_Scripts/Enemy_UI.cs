using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_UI : MonoBehaviour, IHitNumber
{
    [SerializeField] private UI_HP uiHP;
    [SerializeField] private UI_Status uiStatus;
    [SerializeField] private Hit_Number_Pool hitNumberPool;

    [SerializeField] private RectTransform enemyPosition;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image enemyImage;
    private Vector3 originalEnemyPosition;

    [Header("Appear")]
    [SerializeField] private float appearDuration;
    [SerializeField] private float appearSlideDistance;

    [Header("Defeat")]
    [SerializeField] private float defeatShakeDuration;
    [SerializeField] private float defeatShakeMagnitude;
    [SerializeField] private float fallDistance;
    [SerializeField] private float slideDistance;

    private void Awake()
    {
        originalEnemyPosition = enemyPosition.position;
    }

    public void SetEnemyUI(Enemy_SO enemySO, int currentHP, int maxHP)
    {
        enemyImage.sprite = enemySO.enemySprite;
        nameText.text = enemySO.enemyName;
        UpdateHPUI(currentHP, maxHP);
    }

    public void UpdateHPUI(int currentHP, int maxHP, int amount = 0) => uiHP.UpdateHP(currentHP, maxHP, amount);

    public void ShowHitNumber(int damage, bool isBlocked = false) => hitNumberPool.ShowHitNumber(damage, isBlocked);

    public void UpdateCombatStatusUI(int block, int stackingDamage) => uiStatus.UpdateCombatStatus(block, stackingDamage);

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
