using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement_Box : MonoBehaviour
{
    [SerializeField] private Collider2D judgeBoxCollider;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private LayerMask noteLayer;
    private Vector3 originalScale;

    private void Awake()
    {
        judgeBoxCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
    }

    public void TryHitNote()
    {
        StartCoroutine(hitRespond());

        Collider2D[] hits = Physics2D.OverlapBoxAll(judgeBoxCollider.bounds.center, judgeBoxCollider.bounds.size, 0, noteLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<Note>().OnNoteHit();
            Combat_Events.NoteHit(hits[0].GetComponent<Note>().noteSO);
        }
        else
        {
            Combat_Events.NoteMiss();
            sr.color = Color.red;
        }
    }

    private IEnumerator hitRespond()
    {
        transform.localScale = originalScale * 1.2f;

        yield return new WaitForSeconds(0.1f);

        transform.localScale = originalScale;
        sr.color = Color.white;
    }
}
