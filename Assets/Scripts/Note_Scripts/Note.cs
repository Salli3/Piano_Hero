using UnityEngine;

public class Note : MonoBehaviour
{
    public Note_SO noteSO;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D noteCollider;
    [SerializeField] private Animator anim;

    private void Update()
    {
        if (Game_Manager.instance.combatManager.isCombatActive == false) Destroy(gameObject);
        if (noteSO == null) return;

        transform.position += Vector3.down * noteSO.noteSpeed * Game_Manager.instance.combatManager.GetDifficultyLevel() * Time.deltaTime;
    }

    public void Init(Note_SO chosenNote, Transform spawnPoint)
    {
        noteSO = chosenNote;
        transform.position = spawnPoint.position;
        sr.color = noteSO.noteColor;
        noteCollider.enabled = true;
    }

    public void OnNoteHit()
    {
        sr.color = Color.white;
        noteCollider.enabled = false;
        anim.Play("Fade");
        Destroy(gameObject, 0.3f);
    }
}
