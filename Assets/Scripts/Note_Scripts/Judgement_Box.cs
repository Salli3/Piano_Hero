using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement_Box : MonoBehaviour
{
    [SerializeField] private Collider2D judgeBoxCollider;
    [SerializeField] private LayerMask noteLayer;
    public KeyCode triggerKey;

    private void Awake()
    {
        judgeBoxCollider = GetComponent<BoxCollider2D>();
    }

    public void GetOverLappingNote()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(judgeBoxCollider.bounds.center, judgeBoxCollider.bounds.size, 0, noteLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<Note>().OnNoteHit();
        }
    }
}
