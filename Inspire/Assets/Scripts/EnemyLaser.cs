using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    float speed = 40f;
    float damageOfProj;
    bool hasHit = false;
    Stats owner;
    float lifetime = 5f;
    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        this.transform.position += this.transform.up * step;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit)
        {
            return;
        }
        if (collision.GetComponent<PlayerController>() != null)
        {
            hasHit = true;
            collision.GetComponent<Shootable>().TakeDamage(damageOfProj);
            Destroy(this.gameObject);
        }
    }

    public void InitializeBullet(Stats ownerOfBullet, float damageSent)
    {
        damageOfProj = damageSent;
        owner = ownerOfBullet;
    }

}
