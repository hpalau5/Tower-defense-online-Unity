using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoison : BulletFather
{
    //los parametros se le pasan al instanciarla para poder modificar la velocidad y el daño en cada bala
    //si esta activo un bufo
    float poisonDuration;
    float poisonDamage;
    float poisonTickRate; 
    public BulletPoison() : base()
    {
    }
    void Awake()
    {
        this.poisonDamage = transform.parent.GetComponent<TowerPoison>().poisonDamage;
        this.poisonDuration = transform.parent.GetComponent<TowerPoison>().poisonDuration;
        this.poisonTickRate = transform.parent.GetComponent<TowerPoison>().poisonTickRate;
    }

    public override void Update()
    {
        if (enemyTarget != null && enemyTarget.activeSelf)
        {
            transform.position += (enemyTarget.transform.position - this.gameObject.transform.position).normalized * speed * Time.deltaTime;
            if ((enemyTarget.transform.position - transform.position).sqrMagnitude <= distanceForCollideWithEnemy)
            {
                enemyTarget.gameObject.GetComponent<EnemyFather>().DamageEnemy(poisonDamage);
                enemyTarget.gameObject.GetComponent<EnemyFather>().PoisonedEnemy(poisonDamage,poisonDuration,poisonTickRate);
                DestroyBullet();
            }
        }
        else
        {
            DestroyBullet();
        }
    }
}

