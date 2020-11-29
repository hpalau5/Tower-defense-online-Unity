using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCannon : TowerFather
{
    public GameObject bulletPrefab;
    public Transform spawnPointBullet;
    public SpriteRenderer rangeRenderer;

    public float bulletArea { get; protected set; } = 2f;
    public TowerCannon() : base(5f, 5f, 1.8f, 5f, 100) { }

    public override void Awake()
    {
        rangeRenderer.transform.localScale = new Vector2(range, range);
    }
    public override void EnableRangeSprite(bool enable)
    {
        rangeRenderer.enabled = enable;
    }
    public override void Shoot()
    {
        if (bulletQueue.Count > 0)
        {
            GameObject bulletInstance = bulletQueue.Dequeue();
            bulletInstance.GetComponent<BulletFather>().ResetBulletForRespawn(nearestEnemy, base.damage, base.bulletSpeed, spawnPointBullet.position);
            bulletInstance.SetActive(true);
        }
        else
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, spawnPointBullet.transform.position, Quaternion.identity, transform);
            bulletInstance.GetComponent<BulletFather>().InstanceNewBullet(nearestEnemy, base.damage, base.bulletSpeed);
        }
    }
}
