using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public State state;


    [SerializeField] Transform thrusterEffect;
    Vector3 randomDirection;
    [SerializeField] Transform firePoint1, firePoint2;


    Stats stats;

    [SerializeField] Transform laserPrefab;

    float range;
    
    float shotTimer = 1f;
    Transform currentTarget = null;
    float distanceBetweenPlayerAndTarget;
    [SerializeField] float baseAttackDamage = 10f;

    float randomSelectionTime;
    float selectionShotTimer = 0f;
    bool hasSelectedRandomTimeToWaitWhenShootableIsWithinRange = false;
    float visionRange = 60f;
    float moveSpeed;
    public enum State
    {
        Roaming,
        ShootableTargeted,
        PlayerWithinRange,
        ConsumableTargeted
    }

    private void Awake()
    {
        stats = this.GetComponent<Stats>();
        currentTarget = FindObjectOfType<PlayerController>().transform;
        range = Random.Range(stats.range - 10, stats.range + 10);
        moveSpeed = Random.Range(stats.moveSpeed - 19, stats.moveSpeed);
    }
    // Start is called before the first frame update
    void Start()
    {
        randomDirection = Random.insideUnitCircle.normalized;
        state = State.ShootableTargeted;
        shotTimer = stats.fireRate;
    }

    void Update()
    {
        switch (state)
        {
            case State.Roaming:
                HandleMovement();
                ScanForItemsWithinRange();
                HandleReload();
                break;
            case State.ShootableTargeted:
                HandleGoingToAndShootingTarget();
                HandleReload();
                break;
            case State.ConsumableTargeted:
                HandleGoingToConsumable();
                HandleReload();
                break;
        }
    }

    void HandleMovement()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position += randomDirection * step;

        transform.up = randomDirection;
        thrusterEffect.gameObject.SetActive(true);
    }

    void ScanForItemsWithinRange()
    {
        /*Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, visionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.GetComponent<PowerUp>() && hitCollider.transform != this.transform)
            {
                TargetConsumable(hitCollider.transform);
                return;
            }
            if (hitCollider.transform.GetComponent<PlanetBehaviour>() && hitCollider.transform != this.transform)
            {
                SetTarget(hitCollider.transform);
                return;
            }
            if (hitCollider.transform.GetComponent<PlayerShootable>() && hitCollider.transform != this.transform || hitCollider.transform.GetComponent<AIEnemy>() && hitCollider.transform != this.transform)
            {
                SetTarget(hitCollider.transform);
                return;
            }
            
        }*/
        
    }
    void TargetConsumable(Transform targetSent)
    {
        this.currentTarget = targetSent;
        state = State.ConsumableTargeted;
    }
    void SetTarget(Transform targetSent)
    {
        this.currentTarget = targetSent;
        state = State.ShootableTargeted;
    }
    void HandleGoingToConsumable()
    {
        if (currentTarget == null)
        {
            randomDirection = Random.insideUnitCircle.normalized;
            state = State.Roaming;
            return;
        }
        transform.up = currentTarget.position - transform.position;
        distanceBetweenPlayerAndTarget = (currentTarget.position - transform.position).magnitude;
        if (distanceBetweenPlayerAndTarget > 1f)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, step);
        }
    }
    void HandleGoingToAndShootingTarget()
    {
        
        if (currentTarget == null)
        {
            randomDirection = Random.insideUnitCircle.normalized;
            state = State.Roaming;
            return;
        }
        transform.up = currentTarget.position - transform.position;
        distanceBetweenPlayerAndTarget = (currentTarget.position - transform.position).magnitude;
        if (distanceBetweenPlayerAndTarget > range)
        {
            hasSelectedRandomTimeToWaitWhenShootableIsWithinRange = false;
            float step = moveSpeed * Time.deltaTime;
            if (shotTimer > 1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, step);
            }
        }
        else
        {
            if (hasSelectedRandomTimeToWaitWhenShootableIsWithinRange == false)
            {
                randomSelectionTime = Random.Range(0, .5f);
                hasSelectedRandomTimeToWaitWhenShootableIsWithinRange = true;
                selectionShotTimer = 0f;
            }
            HandleShoot();
            //handleshotlogic
        }
    }
    void HandleReload()
    {
        shotTimer += Time.deltaTime;
    }
    void HandleShoot()
    {
        selectionShotTimer += Time.deltaTime;
        if (selectionShotTimer > randomSelectionTime)
        {
            if (shotTimer > stats.fireRate)
            {
                shotTimer = 0f;
                Shoot();

            }
        }
        
    }
    internal void TakeDamage(float damageTaken)
    {
        if (stats.currentHealth <= 0)
        {
            return;
        }
        stats.currentHealth -= damageTaken;
        if (stats.currentHealth <= 0)
        {
            this.GetComponent<PowerUpGenerator>().InstantiateRandomPowerUps();
            Destroy(this.gameObject);
        }
    }
    void Shoot()
    {
        Transform newLaser = Instantiate(laserPrefab, firePoint1.position, firePoint1.rotation);
        newLaser.GetComponent<EnemyLaser>().InitializeBullet(stats, stats.attackPower);
    }


}
