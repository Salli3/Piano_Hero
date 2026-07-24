using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
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

    [SerializeField] private Animator hpBarAnim;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpText;

    private void Awake()
    {
        originalCameraPosition = mainCamera.transform.position;
        originalPlayerPosition = playerPosition.position;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        hpText.text = Mathf.CeilToInt(Game_Manager.instance.statsManager.currentHP) + "/" + Mathf.CeilToInt(Game_Manager.instance.statsManager.maxHP);
        hpBar.maxValue = Game_Manager.instance.statsManager.maxHP;
        hpBar.value = Game_Manager.instance.statsManager.currentHP;
    }

    #region Camera shake methods
    public void Shake()
    {
        hpBarAnim.Play("HP_Decrease");

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
            playerPosition.position = originalPlayerPosition + new Vector3(y * 100, x * 100, 0f);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalCameraPosition;
        playerPosition.position = originalPlayerPosition;
        shakeRoutine = null;
        playerImage.color = Color.white;
    }
    #endregion
}
