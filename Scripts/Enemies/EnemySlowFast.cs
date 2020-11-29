using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowFast : EnemyFather
{    
    public HealthBar healthBar;   
    public static Queue<GameObject> enemiesDeathForReusingGameObjectQueue = new Queue<GameObject>();

    float timeElapsedSinceLastMovementSpeedChanged = 0f;

    float timeFast = 1f;
    float timeSlow = 2f;
    float speedFactorWhenFast=5f;
    float speedFactorWhenSlow=1f;
    float speedFactor;

    public EnemySlowFast() : base(10, 2, 5, 8, 0, false, enemiesDeathForReusingGameObjectQueue)
    {
    }
    public override void Awake()
    {
        ResetEnemyHpBarForSpawn();
    }
    public override void ResetEnemyHpBarForSpawn()
    {
        healthBar.SetMaxHealth((int)base.maxHp);
    }
    public override void UpdateHpBar()
    {
        healthBar.SetHealth((int)base.hp);
    }
    void ChangeSpeedFactor()
    {
        if(Mathf.Approximately(speedFactor,speedFactorWhenFast))
        {
            timeElapsedSinceLastMovementSpeedChanged = timeSlow;
            speedFactor = speedFactorWhenSlow;
        }
        else
        {
            timeElapsedSinceLastMovementSpeedChanged = timeFast;
            speedFactor = speedFactorWhenFast;
        }
    }
    public override void Move()
    {
        timeElapsedSinceLastMovementSpeedChanged-=Time.deltaTime;
        if(timeElapsedSinceLastMovementSpeedChanged < 0)
        {
            ChangeSpeedFactor();
        }

        //quizas seria bueno quitar la Y y que solo se moviera en X Z
        //sqrMagnitudeno hace la raiz, osea si esta a menos de sqrt(0.2) es cuando cambiará
        transform.Translate((Waypoints.waypoints[nextWayPointIndex] - transform.position).normalized * movementSpeed * speedFactor * Time.deltaTime, Space.World);
        if ((Waypoints.waypoints[nextWayPointIndex] - transform.position).sqrMagnitude <= distanceForArrayWaypoint)
        {
            if (Waypoints.waypoints.Length - 1 > nextWayPointIndex)
            {
                nextWayPointIndex++;
            }
            else
            {
                ArriveLastWaypoint();
            }
        }
    }
}
