using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public static AISpawner singleton;

    [SerializeField] GameObject aiEnemyToSpawn;

    public float timer;

    public int totalNumber;

    int currentTen = 0;

    bool tenSecondsHavePassed;
    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
        }
        singleton = this;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            return;
        }
        timer += Time.deltaTime;

        //every 10 second spawn that many units
        if (timer % 10 < 1 && !tenSecondsHavePassed) 
        {
            currentTen += 10;
            Debug.Log(currentTen);
            tenSecondsHavePassed = true;
            SpawnOutsideOfCamera();
        }
        if (timer % 10 > 1)
        {
            tenSecondsHavePassed = false;
        }
    }

    public void StartSpawning()
    {
        Debug.Log(timer % 10);
        for (int i = 0; i < currentTen / 2; i++)
        {
            float spawnY = Random.Range
                (-280, 280);
            //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y) * 8;
            float spawnX = Random.Range
                (-280, 280);
            //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x) * 8;

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            GameObject newEnemy = Instantiate(aiEnemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }
    internal void SpawnRandomEnemy()
    {
        float spawnY = Random.Range
               (-280, 280);
        //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y) * 8;
        float spawnX = Random.Range
            (-280, 280);
        //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x) * 8;

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        GameObject newEnemy = Instantiate(aiEnemyToSpawn, spawnPosition, Quaternion.identity);
        
    }

    public void SpawnOutsideOfCamera()
    {
        Debug.Log(timer % 10);

        for (int i = 0; i < currentTen / 3; i++)
        {
            int side = Random.Range(1, 5);
            float spawnY = 0;
            float spawnX = 0;
            if (side == 1)
            {
                spawnY = Random.Range((Camera.main.transform.position.y - Camera.main.orthographicSize), Camera.main.transform.position.y + Camera.main.orthographicSize);
                spawnX = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect + 100;
            }
            if (side == 2)
            {
                spawnY = Random.Range((Camera.main.transform.position.y - Camera.main.orthographicSize), Camera.main.transform.position.y + Camera.main.orthographicSize);
                spawnX = (Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect) - 100;
            }
            if (side == 3)
            {
                spawnY = Camera.main.transform.position.y + Camera.main.orthographicSize + 100;
                spawnX = Random.Range((Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect), Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect);
            }
            if (side == 4)
            {
                spawnY = Camera.main.transform.position.y - Camera.main.orthographicSize - 100;
                spawnX = Random.Range( (Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect), Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect);
            }
            //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x) * 8;

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            GameObject newEnemy = Instantiate(aiEnemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
