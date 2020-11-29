using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageSlow : BulletFather
{
    //los parametros se le pasan al instanciarla para poder modificar la velocidad y el daño en cada bala
    //si esta activo un bufo
    float speedFreeze;
    float durationFreeze;
    public BulletDamageSlow() : base()
    {
    }
    void Awake()
    {
        this.speedFreeze = transform.parent.GetComponent<TowerDamageSlow>().speedFreeze;
        this.durationFreeze = transform.parent.GetComponent<TowerDamageSlow>().durationFreeze;
    }

    public override void Update()
    {
        if (enemyTarget != null && enemyTarget.activeSelf)
        {
            transform.position += (enemyTarget.transform.position - this.gameObject.transform.position).normalized * speed * Time.deltaTime;
            if ((enemyTarget.transform.position - transform.position).sqrMagnitude <= distanceForCollideWithEnemy)
            {
                enemyTarget.gameObject.GetComponent<EnemyFather>().DamageEnemy(damage);
                enemyTarget.gameObject.GetComponent<EnemyFather>().ChangeSpeedFreeze(speedFreeze,durationFreeze);
                DestroyBullet();
            }
        }
        else
        {
            DestroyBullet();
        }
    }
}

