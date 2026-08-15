using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Note_SO noteSO;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D noteCollider;
    [SerializeField] private Animator anim;
    [SerializeField] private TMP_Text noteName;

    private void FixedUpdate()
    {
        if (Game_Manager.instance.isCombatActive == false)
        {
            Destroy(gameObject);
            enabled = false;
            return;
        }
        if (noteSO == null) return;
        rb.velocity = Vector3.down * noteSO.noteSpeed * Game_Manager.instance.noteSpeed;
    }

    public void Init(Note_SO chosenNote, Transform spawnPoint)
    {
        noteSO = chosenNote;
        transform.position = spawnPoint.position;
        sr.color = noteSO.Color;
        noteCollider.enabled = true;
        noteName.text = noteSO.Name;
    }

    public void OnNoteHit()
    {
        sr.color = Color.white;
        noteCollider.enabled = false;
        anim.Play("Fade");

        Destroy(gameObject, 0.3f);
    }
}