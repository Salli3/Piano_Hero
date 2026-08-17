using UnityEngine;

public class Note_Spawner : MonoBehaviour
{
    [SerializeField] private float width;
    [SerializeField] private float height;

    [Header("Spawn tuning")]
    [SerializeField] private float minInterval;
    [SerializeField] private float maxInterval;
    [SerializeField] private float timer;
    [SerializeField] private float spawnInterval;

    [Header("Spawn ratio")]
    [SerializeField, Range(0f, 1f)] private float playerNoteChance;

    [Header("References")]
    [SerializeField] private Transform spawnerTransform;
    [SerializeField] private Enemy_HP enemyHP;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        spawnInterval = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        if (Game_Manager.instance.isCombatActive == false) return;

        timer += Time.deltaTime * Game_Manager.instance.NoteSpeed;
        if (timer >= spawnInterval)
        {
            timer = 0;
            spawnInterval = Random.Range(minInterval, maxInterval);
            SpawnRandomNote();
        }
    }

    private void SpawnRandomNote()
    {
        Note_SO chosenNote;
        Transform spawnPoint;

        if (Random.value < playerNoteChance)
        {
            Note_SO[] playerNotes = Game_Manager.instance.statsManager.noteLevelTracker.GetNote();
            chosenNote = playerNotes[Random.Range(0, playerNotes.Length)];
            spawnPoint = spawnPoints[Random.Range(0, 2)];
        }
        else
        {
            Note_SO[] enemyNotes = Game_Manager.instance.CurrentEnemy.attackTypes;
            chosenNote = enemyNotes[Random.Range(0, enemyNotes.Length)];
            spawnPoint = spawnPoints[Random.Range(2, 4)];
        }

        Note note = Instantiate(notePrefab, transform).GetComponent<Note>();
        note.Init(chosenNote, spawnPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnerTransform.position, new Vector3(width, height, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawnPoints[0].position, new Vector3(1, 1, 0));
        Gizmos.DrawWireCube(spawnPoints[1].position, new Vector3(1, 1, 0));
        Gizmos.DrawWireCube(spawnPoints[2].position, new Vector3(1, 1, 0));
        Gizmos.DrawWireCube(spawnPoints[3].position, new Vector3(1, 1, 0));
    }
}