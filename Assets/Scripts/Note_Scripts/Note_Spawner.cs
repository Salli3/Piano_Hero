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
    [SerializeField] private Combat_Manager combatManager;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        spawnInterval = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        if (Game_Manager.instance.isCombatActive == false) return;

        timer += Time.deltaTime * Game_Manager.instance.GetDifficultyLevel();
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

        if (Random.value < playerNoteChance)
        {
            Note_SO[] playerNotes = Game_Manager.instance.statsManager.playerAttackTypes.ToArray();
            chosenNote = playerNotes[Random.Range(0, playerNotes.Length)];
        }
        else
        {
            Note_SO[] enemyNotes = combatManager.currentEnemy.attackTypes;
            chosenNote = enemyNotes[Random.Range(0, enemyNotes.Length)];
        }

        Note note = Instantiate(notePrefab).GetComponent<Note>();
        note.Init(chosenNote, spawnPoints[Random.Range(0, spawnPoints.Length)]);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawnPoints[0].position, new Vector3(1, 1, 0));
        Gizmos.DrawWireCube(spawnPoints[1].position, new Vector3(1, 1, 0));
        Gizmos.DrawWireCube(spawnPoints[2].position, new Vector3(1, 1, 0));
        Gizmos.DrawWireCube(spawnPoints[3].position, new Vector3(1, 1, 0));
    }
}