using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCannon : BulletFather
{
    //esta torre tiene un parametro mas que es bulletArea
    public float bulletArea;
    public BulletCannon() : base()
    {
    }

    void Awake()
    {
        this.bulletArea = transform.parent.GetComponent<TowerCannon>().bulletArea;
    }
    //este no va hacia el enemigo sino que va hacia la direccion inicial donde ha sido lanzada
    //al llegar hace daño en area
    public override void Update()
    {
        if (enemyTarget != null && enemyTarget.activeSelf)
        {
            transform.position += (targetPosition - this.gameObject.transform.position).normalized * speed * Time.deltaTime;
           
            if ((targetPosition - transform.position).sqrMagnitude <= distanceForCollideWithEnemy)
            {
                foreach (Collider col in Physics.OverlapSphere(transform.position, bulletArea, TowerFather.layerMask))
                {
                    col.gameObject.GetComponent<EnemyFather>().DamageEnemy(base.damage);
                }
                DestroyBullet();
            }
        }
        else
        {
            DestroyBullet();
        }
    }
}

