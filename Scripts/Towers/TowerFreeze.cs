using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFreeze : TowerFather
{
    public Transform spawnPointBullet;
    public SpriteRenderer rangeRenderer;

    public float speedFreeze { get; protected set; } = 0.6f;
    public float durationFreeze { get; protected set; } = 1f;
    public TowerFreeze() : base(0f, 10f, 1f, 20f, 80) { }

    public override void Awake()
    {
        rangeRenderer.transform.localScale = new Vector2(range, range);
    }
    public override void EnableRangeSprite(bool enable)
    {
        rangeRenderer.enabled = enable;
    }

    public override void FindEnemy()
    {
        //si ha pasado el tiempo para el siguiente ataque
        lastAtk -= Time.deltaTime;
        if (lastAtk <= 0)
        {           
            lastAtk = atkSpeed;
            //Shoot();
            foreach (Collider col in ReturnAllNearestEnemies())
            {
                col.gameObject.GetComponent<EnemyFather>().ChangeSpeedFreeze(speedFreeze,durationFreeze);
            }
        }
    }
    public override void Shoot()
    {
        //este no dispara como tal, hace la animacion de congelar, tambien puede sacar una bala que haga eso?
    }
}
