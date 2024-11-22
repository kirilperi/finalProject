using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public GameObject capsuleEnemyPrefab;
    int wave;
    bool gameRuning;
    
    public static List<GameObject> enemyList = new List<GameObject>();
    
    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Make the cursor invisible
        Cursor.visible = false;

        wave = 0;
        gameRuning = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseLockControll();
        SpawnManager();
    }

    void MouseLockControll()
    {
        // Unlock and show the cursor if the Escape key is pressed (optional)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Relock and hide the cursor if the L key is pressed (optional)
        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }


    void SpawnManager()
    {
        if(!gameRuning)
        {
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
            int enemyCount = Mathf.FloorToInt(20 + wave * 1.5f * 20);
            for(int i=0;i<enemyCount;i++)
            {
                int randomSpawn = Random.Range(0, spawnPoints.Length);
                Instantiate(capsuleEnemyPrefab, spawnPoints[randomSpawn].transform);
            }
            gameRuning = true;
            
        }

        if(enemyList.Count==0)
        {
            gameRuning = false;
        }
        
    }
}
