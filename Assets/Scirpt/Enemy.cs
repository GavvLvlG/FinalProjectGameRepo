using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Drag your enemy prefab here in the Inspector
    public float respawnDelay = 3f; // Time in seconds before respawning
    public Transform spawnPoint; // Drag an empty GameObject here for the spawn location

    // Call this function from the enemy's death logic
    public void StartRespawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(respawnDelay);

        // Instantiate a new enemy at the spawn point's position and rotation
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
