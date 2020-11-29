using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class HeroeFather : MonoBehaviour
{
    public Transform target;

	protected float hp;
    float maxHp;
    float movementSpeed;
    float damage;
    float atkSpeed;
    float armor;
    float range;

    bool heroeMoving = false;
    public HeroeFather(float hp, float movementSpeed, float damage, float atkSpeed, float armor, float range)
    {
        this.hp = hp;
        this.maxHp = hp;
        this.movementSpeed = movementSpeed;
        this.damage = damage;
        this.atkSpeed = atkSpeed;
        this.range = range;
    }

#region Methods to override

    public abstract void Awake();
    public abstract void OnTriggerEnter(Collider other);
    
#endregion
    public virtual void Update()
    {
        if(heroeMoving)
            Move();
    }

    public virtual void Move()
    {
        //quizas seria bueno quitar la Y y que solo se moviera en X Z
		//sqrMagnitudeno hace la raiz, osea si esta a menos de sqrt(0.2) es cuando cambiará
        transform.Translate((target.position - transform.position).normalized * movementSpeed * Time.deltaTime, Space.World);
        if ((target.position-transform.position).sqrMagnitude <= 0.1f)
        {
           heroeMoving = false;
        }
    }
    public void ClickMove(){
        heroeMoving = true;
    }

    public virtual void Attack()
    {
        //if(!heroeMoving)
            //attack
    }
    public virtual void DamageReceived()
    {

    }
    public virtual void HeroeDie()
    {
        
    }
}
