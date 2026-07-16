using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private Note_SO noteSO;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D noteCollider;

    private void Update()
    {
        if (noteSO == null) return;

        transform.position += Vector3.down * noteSO.noteSpeed * Game_Manager.instance.GetDifficultyLevel() * Time.deltaTime;
    }

    public void Init(Note_SO chosenNote, Transform spawnPoint)
    {
        noteSO = chosenNote;
        transform.position = spawnPoint.position;
        sr.color = noteSO.noteColor;
    }
}
