using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrange : EnemyFather
{
    public HealthBar healthBar;
    public static Queue<GameObject> enemiesDeathForReusingGameObjectQueue = new Queue<GameObject>();

    public EnemyOrange() : base(4, 12, 2, 5, 0, false, enemiesDeathForReusingGameObjectQueue)
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
}
