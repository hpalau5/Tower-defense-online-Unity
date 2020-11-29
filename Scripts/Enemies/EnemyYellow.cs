using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYellow : EnemyFather
{
    public HealthBar healthBar;
    public static Queue<GameObject> enemiesDeathForReusingGameObjectQueue = new Queue<GameObject>();

    public EnemyYellow() : base(100, 1, 5, 100, 0, false, enemiesDeathForReusingGameObjectQueue)
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
