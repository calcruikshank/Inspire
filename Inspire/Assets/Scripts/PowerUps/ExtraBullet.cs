using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/ExtraBullet")]
public class ExtraBullet : PowerUpEffect
{
    public int amount = 1;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Stats>().numOfBullets += amount;
        
    }
}
