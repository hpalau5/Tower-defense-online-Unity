using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class SpellFather : MonoBehaviour
{
    public Vector3 destinationPosition;
    public readonly static int enemyLayer = 8;
    public readonly static int layerMask = 1 << enemyLayer;

    public float damage { get; private set; }
    public float cd { get; private set; }
    protected float area { get; private set; }
    protected float speed { get; private set; }


    /*a parte tendran efectos como congelar, que caigan oleadas de meteoritos, aumentar el dps de las torres, 
        ralentizarlos, invocar un golem en el sitio...
    */
    public SpellFather(float damage, float cd, float area, float speed)
    {
        this.damage = damage;
        this.cd = cd;
        this.area = area;
        this.speed = speed;
    }

    public abstract void Update();

    public abstract void SetTargetPosition(Vector3 pos);
    public virtual void OnTriggerEnter(Collider other)
    {
      //si fuera algun tipo de mina que desaparece al explotar, si no desaparece cuando acabe
    }
}
