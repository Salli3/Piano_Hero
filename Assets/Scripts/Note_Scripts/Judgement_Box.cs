using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement_Box : MonoBehaviour
{
    [SerializeField] private Collider2D judgeBoxCollider;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private LayerMask noteLayer;
    public KeyCode triggerKey;

    [SerializeField] private Player_HP playerHP;

    private void Awake()
    {
        judgeBoxCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void GetOverLappingNote()
    {
        StartCoroutine(hitRespond());

        Collider2D[] hits = Physics2D.OverlapBoxAll(judgeBoxCollider.bounds.center, judgeBoxCollider.bounds.size, 0, noteLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<Note>().OnNoteHit();
        }
        else
        {
            playerHP.ChangeHP(0);
            sr.color = Color.red;
        }
    }

    private IEnumerator hitRespond()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 1.2f;

        yield return new WaitForSeconds(0.1f);

        transform.localScale = originalScale;
        sr.color = Color.white;
    }
}
