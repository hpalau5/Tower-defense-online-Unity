using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeoreGreen : HeroeFather
{
    public HealthBar healthBar;
    public HeoreGreen() : base(100, 4, 50, 1, 0, 2)
    {
    }
    public override void Awake()
    {
        healthBar.SetMaxHealth((int)base.hp);
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            base.hp -= other.gameObject.GetComponent<BulletFather>().damage;
            healthBar.SetHealth((int)base.hp);
            if (base.hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
