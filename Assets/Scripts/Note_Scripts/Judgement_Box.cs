using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Judgement_Box : MonoBehaviour
{
    [SerializeField] private Collider2D judgeBoxCollider;
    [SerializeField] private SpriteRenderer sr;
    public TMP_Text inputText;
    [SerializeField] private LayerMask noteLayer;
    private Vector3 originalScale;

    public static event Action<Note_SO> OnNoteHit;
    public static event Action OnNoteMiss;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void TryHitNote()
    {
        StartCoroutine(hitRespond());

        Collider2D[] hits = Physics2D.OverlapBoxAll(judgeBoxCollider.bounds.center, judgeBoxCollider.bounds.size, 0, noteLayer);
        if (hits.Length > 0)
        {
            foreach (Collider2D hit in hits)
            {
                hit.GetComponent<Note>().OnNoteHit();
                OnNoteHit?.Invoke(hit.GetComponent<Note>().noteSO);
            }
        }
        else
        {
            OnNoteMiss.Invoke();
            sr.color = Color.red;
        }
    }

    private IEnumerator hitRespond()
    {
        transform.localScale = originalScale * 1.2f;

        yield return new WaitForSecondsRealtime(0.1f);

        transform.localScale = originalScale;
        sr.color = Color.white;
    }
}
