using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{

    public static PlanetSpawner singleton;
    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
        }
        singleton = this;
    }
    [SerializeField] Transform[] planetsToSpawn;
    void Start()
    {
        for (int i = 0; i < 1000; i++)
        {
            float spawnY = Random.Range
                (-4000, 4000);
                //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y) * 8;
            float spawnX = Random.Range
                (-4000, 4000);
            //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x) * 8;

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            Instantiate(planetsToSpawn[Random.Range(0, planetsToSpawn.Length)], spawnPosition, Quaternion.identity);
        }
    }

    public void SpawnRandomPlanet()
    {
        float spawnY = Random.Range
                (-4000, 4000);
        //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y) * 8;
        float spawnX = Random.Range
            (-4000, 4000);
        //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x) * 8;

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        Instantiate(planetsToSpawn[Random.Range(0, planetsToSpawn.Length)], spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
