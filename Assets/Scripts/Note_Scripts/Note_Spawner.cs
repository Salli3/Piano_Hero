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

    [Header("References")]
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        spawnInterval = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        if (Game_Manager.instance.combatManager.isCombatActive == false) return;

        timer += Time.deltaTime * Game_Manager.instance.combatManager.GetDifficultyLevel();
        if (timer >= spawnInterval)
        {
            timer = 0;
            spawnInterval = Random.Range(minInterval, maxInterval);
            SpawnRandomNote();
        }
    }

    private void SpawnRandomNote()
    {
        Note_SO chosenNote = Game_Manager.instance.combatManager.currentNotes[Random.Range(0, Game_Manager.instance.combatManager.currentNotes.Length)];

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
