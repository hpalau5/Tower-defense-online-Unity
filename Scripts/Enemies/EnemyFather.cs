using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class EnemyFather : MonoBehaviour
{
    //todo lo que cambie al estar vivo, como la posicion, la hp o la speed se tiene que reiniciar en ResetEnemyForSpawn

    public Queue<GameObject> enemiesQueued = new Queue<GameObject>();

    public int nextWayPointIndex { get; protected set; } = 0;
    public float maxHp;
    public float initialMovementSpeed;
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected float movementSpeed;
    protected float nexusDmg;
    protected float armor;
    public int moneyDrop { get; private set; }
    Boolean flyingType;
    public Boolean poisoned;
    public float polla;
    public readonly static float distanceForArrayWaypoint = 0.1f;

    float lastTimeFreezed = 0f;
    float howMuchIsSlowed = 0f;
    float lastTimePoisoned = 0f;
    float poisonTickRate;
    float poisonDmg;
    float poisonDuration;
    public EnemyFather(float hp, float movementSpeed, float nexusDmg, int moneyDrop, float armor, Boolean flyingType, Queue<GameObject> enemies)
    {
        this.hp = hp;
        this.maxHp = hp;
        this.movementSpeed = movementSpeed;
        this.initialMovementSpeed = movementSpeed;
        this.nexusDmg = nexusDmg;
        this.moneyDrop = moneyDrop;
        this.armor = armor;
        this.flyingType = flyingType;
        this.enemiesQueued = enemies;
    }


    #region Methods to override

    public abstract void Awake();
    public abstract void ResetEnemyHpBarForSpawn();
    public abstract void UpdateHpBar();


    #endregion

    public virtual void Update()
    {
        if (!Mathf.Approximately(howMuchIsSlowed, 0f))
        {
            lastTimeFreezed -= Time.deltaTime;
            if (lastTimeFreezed < 0f)
            {
                movementSpeed = initialMovementSpeed;
                lastTimeFreezed = 0f;
                howMuchIsSlowed = 0f;
            }
        }
        if (poisoned) {
            lastTimePoisoned += Time.deltaTime;
            if( lastTimePoisoned > poisonTickRate && poisonDuration>0) 
            {
                DamageEnemy(poisonDmg);
                poisonDuration -= poisonTickRate;
                lastTimePoisoned = 0;
            }
        }
        Move();
    }
    public virtual void ResetEnemyForSpawn(Vector3 position)
    {
        hp = maxHp;
        movementSpeed = initialMovementSpeed;
        transform.position = position;
        nextWayPointIndex = 0;
        lastTimeFreezed = 0f;
        poisoned = false;
        ResetEnemyHpBarForSpawn();
    }
    public virtual void Move()
    {
        //quizas seria bueno quitar la Y y que solo se moviera en X Z
        //sqrMagnitudeno hace la raiz, osea si esta a menos de sqrt(0.2) es cuando cambiará
        transform.Translate((Waypoints.waypoints[nextWayPointIndex] - transform.position).normalized * movementSpeed * Time.deltaTime, Space.World);
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

    public virtual void ArriveLastWaypoint()
    {
        if (Nexus.instance)
        {
            Nexus.instance.NexusDmg(nexusDmg);
        }
        DestroyEnemy();
    }

    public virtual void EnemyDeadDropGold()
    {
        GameManager.instance.AddGold(moneyDrop);
    }
    public virtual void DestroyEnemy()
    {
        EnemySpawner.instance.RemoveEnemy(gameObject);
        gameObject.SetActive(false);
        enemiesQueued.Enqueue(gameObject);
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("spell"))
        {
            DamageEnemy(other.gameObject.GetComponent<SpellFather>().damage);
        }
    }
    public void DamageEnemy(float damageDone)
    {
        hp -= damageDone;
        if (hp <= 0)
        {
            EnemyDeadDropGold();
            DestroyEnemy();
        }
        else
        {
            UpdateHpBar();
        }
    }
    public void ChangeSpeedFreeze(float speedFreeze, float durationFreeze)
    {
        //si es igual o mayor se le aplica, sino no
        //si el slow aplicado es mas fuerte que el que tiene, es el mismo o no tenia slow
        if (speedFreeze <= howMuchIsSlowed || Mathf.Approximately(speedFreeze - howMuchIsSlowed, 0f) || Mathf.Approximately(howMuchIsSlowed, 0f))
        {
            howMuchIsSlowed = speedFreeze;
            lastTimeFreezed = durationFreeze;
            movementSpeed = initialMovementSpeed * speedFreeze;
        }
    }
    public void PoisonedEnemy(float poisonDmg, float poisonDuration, float poisonTickRate)
    {
        this.poisonDmg = poisonDmg;
        this.poisonDuration = poisonDuration;
        this.poisonTickRate = poisonTickRate;
        poisoned = true;
    }
}
