using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 lastLookedPosition;
    Vector2 lookDirection;
    Vector3 movement;
    Vector2 inputMovement;
    Vector2 directionToShoot;
    Vector2 lastShotDirection;
    
    float shotTimer;
    [SerializeField] Transform gunTransform;
    Stats stats;

    [SerializeField] Transform laser;
    [SerializeField] Transform firePoint;

    bool fire = false;
        
    public State state;
    public enum State
    {
        Normal
    }

    void Start()
    {
        FollowTargetTransform.singleton.SetTarget(transform);
        stats = this.GetComponent<Stats>();
        rb = this.GetComponent<Rigidbody2D>();
        shotTimer = stats.fireRate;
        state = State.Normal;
    }

    void Update()
    {
        switch (state)
        {
            case State.Normal:
                FaceLookDirection();
                HandleMovement();
                HandleShoot();
                HandleReload();
                break;
        }
    }
    void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                FixedHandleMovement();
                break;
        }
    }

    private void HandleMovement()
    {
        movement.x = inputMovement.x;
        movement.y = inputMovement.y;
        float step = stats.moveSpeed * Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, movement, step);
        transform.position += movement * step;
        transform.up = lastLookedPosition;
    }

    internal void SetStateToNormal()
    {
        throw new NotImplementedException();
    }

    private void FixedHandleMovement()
    {
    }
    void FaceLookDirection()
    {
        Vector3 lookTowards = new Vector3(lookDirection.x, lookDirection.y);
        if (lookTowards.magnitude != 0f)
        {
            lastLookedPosition = lookTowards.normalized;
        }
        transform.right = Vector2.MoveTowards(transform.right, lastLookedPosition, .1f);

    }
    void HandleReload()
    {
        shotTimer += Time.deltaTime;
    }
    void HandleShoot()
    {
        Vector3 shootTowards = new Vector3(directionToShoot.x, directionToShoot.y);
        if (directionToShoot.magnitude != 0f)
        {
            //gunTransform.up = Vector2.MoveTowards(gunTransform.up, -lastShotDirection, 165 * Time.deltaTime);
            gunTransform.up = -lastShotDirection;
            lastShotDirection = shootTowards.normalized;
        }
        if (fire)
        {
            if (shotTimer > stats.fireRate)
            {
                shotTimer = 0f;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        int startingRotationMultiplier = 0;
        if (stats.numOfBullets > 1)
        {
            if (stats.numOfBullets % 2 == 1)
            {
                startingRotationMultiplier = (stats.numOfBullets - 1) / 2;
            }
            else
            {
                startingRotationMultiplier = (stats.numOfBullets) / 2;
            }
        }
        if (stats.numOfBullets % 2 == 1)
        {
            Transform newLaser = Instantiate(laser, firePoint.position, firePoint.rotation);
            newLaser.localEulerAngles = new Vector3(newLaser.localEulerAngles.x, newLaser.localEulerAngles.y, newLaser.localEulerAngles.z);
            newLaser.GetComponent<LaserBehaviour>().InitializeBullet(stats, stats.attackPower);
        }
        for (int i = 1; i < startingRotationMultiplier + 1; i++)
        {
            //change the angle based on the number of bullets

            //if num of bullets is five get the middle number

            Transform newLaser = Instantiate(laser, firePoint.position, firePoint.rotation);
            newLaser.localEulerAngles = new Vector3(newLaser.localEulerAngles.x, newLaser.localEulerAngles.y, newLaser.localEulerAngles.z + (2 * i));
            newLaser.GetComponent<LaserBehaviour>().InitializeBullet(stats, stats.attackPower);
        }
        for (int i = 1; i < startingRotationMultiplier + 1; i++)
        {
            //change the angle based on the number of bullets

            //if num of bullets is five get the middle number

            Transform newLaser = Instantiate(laser, firePoint.position, firePoint.rotation);
            newLaser.localEulerAngles = new Vector3(newLaser.localEulerAngles.x, newLaser.localEulerAngles.y, newLaser.localEulerAngles.z - (2 * i));
            newLaser.GetComponent<LaserBehaviour>().InitializeBullet(stats, stats.attackPower);
        }
    }

    public void TakeDamage(float damageTaken)
    {
        if (stats.currentHealth <= 0)
        {
            return;
        }
        stats.currentHealth -= damageTaken;
        if (stats.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnMove(InputValue value)
    {
        inputMovement = value.Get<Vector2>();
        lookDirection = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        directionToShoot = value.Get<Vector2>();
    }

    void OnFire()
    {
        Debug.Log("Fire");
        fire = true;
    }

    void OnFireReleased()
    {
        fire = false;
    }
}
