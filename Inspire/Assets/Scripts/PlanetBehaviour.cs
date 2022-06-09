using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : Shootable
{
    bool hasDied = false;
    public override void TakeDamage(float damageTaken)
    {
        if (hasDied)
        {
            return;
        }
        health -= damageTaken;
        if (health <= 0)
        {
            hasDied = true;
            /*float spawnY = Random.Range
                   (-280, 280);
            //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y) * 8;
            float spawnX = Random.Range
                (-280, 280);

            this.transform.position = new Vector2(spawnX, spawnY);
            health = startingHealth;*/
            this.GetComponent<PowerUpGenerator>().InstantiateRandomPowerUps();
            //PlanetSpawner.singleton.SpawnRandomPlanet();
            Destroy(this.gameObject);
        }
    }
}
