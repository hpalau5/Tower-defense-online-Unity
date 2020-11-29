using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    [SerializeField]
    public float hp = 100;
    public HealthBar healthBar;

    public static Nexus instance;
    void Start()
    {
        healthBar.SetMaxHealth((int)hp);
    }
    private void Awake()
    {
        instance = this;
    }
    public void NexusDmg(float dmg)
    {
        hp -= dmg;
        healthBar.SetHealth((int)hp);
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
