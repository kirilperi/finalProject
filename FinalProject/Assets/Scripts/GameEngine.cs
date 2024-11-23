using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public List<GameObject> EnemyPrefabs;
    private int wave;
    private bool gameRunning;

    public static List<GameObject> enemyList = new List<GameObject>();

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Make the cursor invisible

        wave = 0;
        gameRunning = false;
    }

    void Update()
    {
        MouseLockControl();

        // Start spawning if not already running and no enemies exist
        if (!gameRunning && enemyList.Count == 0)
        {
            StartCoroutine(SpawnManager());
        }
    }

    void MouseLockControl()
    {
        // Toggle cursor lock state with Escape and L keys
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private IEnumerator SpawnManager()
    {
        gameRunning = true;

        // Find all spawn points tagged "Spawn"
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        int enemyCount = Mathf.FloorToInt(20 + wave * 1.5f * 20);

        // Spawn enemies at random points
        for (int i = 0; i < enemyCount; i++)
        {
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            int randomEnemy = Random.Range(0, EnemyPrefabs.Count);

            // Instantiate enemy at the chosen spawn point
            Instantiate(EnemyPrefabs[randomEnemy], spawnPoints[randomSpawn].transform.position, Quaternion.identity);

            yield return new WaitForSeconds(0.3f); // Delay between spawns
        }

        wave++; // Increment the wave number

        // Wait until all enemies are defeated
        while (enemyList.Count > 0)
        {
            yield return null; // Wait until the next frame to check again
        }

        gameRunning = false; // Allow next wave to start
    }
}
