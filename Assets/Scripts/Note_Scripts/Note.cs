using UnityEngine;

public class Note : MonoBehaviour
{
    public Note_SO noteSO;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D noteCollider;
    [SerializeField] private Animator anim;
    [SerializeField] private Note_Display noteDisplay;

    private void FixedUpdate()
    {
        if (Game_Manager.instance.isCombatActive == false)
        {
            Destroy(gameObject);
            enabled = false;
            return;
        }
        if (noteSO == null) return;
        rb.velocity = Vector3.down * noteSO.noteSpeed * Game_Manager.instance.GetDifficultyLevel();
    }

    public void Init(Note_SO chosenNote, Transform spawnPoint)
    {
        noteSO = chosenNote;
        transform.position = spawnPoint.position;
        sr.color = noteSO.noteColor;
        noteCollider.enabled = true;

        noteDisplay.SetNote(noteSO);
    }

    public void OnNoteHit()
    {
        sr.color = Color.white;
        noteCollider.enabled = false;
        anim.Play("Fade");
        Destroy(gameObject, 0.3f);
    }
}