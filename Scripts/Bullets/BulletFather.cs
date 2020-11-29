using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BulletFather : MonoBehaviour
{
    // el enemy es para las balas que siguen, el target para las que van al punto determinado al dispararse
    protected GameObject enemyTarget { get; private set; }
    protected Vector3 targetPosition { get; private set; }
    public float speed { get; private set; }
    public float damage { get; private set; }

    public readonly static float distanceForCollideWithEnemy = 0.4f;

    public BulletFather()
    {
    }

    #region Methods to override
    #endregion 
   public virtual void ResetBulletForRespawn(GameObject enemyGameObject, float damage, float speed, Vector3 position)
    {
        this.enemyTarget = enemyGameObject;
        this.targetPosition = enemyGameObject.transform.position;
        this.damage = damage;
        this.speed = speed;
        gameObject.transform.position = position;
    }

    public virtual void InstanceNewBullet(GameObject enemyGameObject, float damage, float speed)
    {
        this.enemyTarget = enemyGameObject;
        this.targetPosition = enemyGameObject.transform.position;
        this.damage = damage;
        this.speed = speed;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (enemyTarget != null && enemyTarget.activeSelf)
        {
            transform.position += (enemyTarget.transform.position - this.gameObject.transform.position).normalized * speed * Time.deltaTime;
            if ((enemyTarget.transform.position - transform.position).sqrMagnitude <= distanceForCollideWithEnemy)
            {
                enemyTarget.gameObject.GetComponent<EnemyFather>().DamageEnemy(damage);
                DestroyBullet();
            }
        }
        else
        {
            DestroyBullet();
        }
    }

    public virtual void DestroyBullet()
    {
        gameObject.SetActive(false);
        transform.parent.GetComponent<TowerFather>().Enqueue(gameObject);
    }
}
