using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGreen : TowerFather
{
    public GameObject bulletPrefab;
    public Transform spawnPointBullet;
    public SpriteRenderer rangeRenderer;

    public TowerGreen() : base(1f, 10f, 0.2f, 20f, 80) { }

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
            Instantiate(bulletPrefab, spawnPointBullet.transform.position, Quaternion.identity, transform).
            GetComponent<BulletFather>().InstanceNewBullet(nearestEnemy, base.damage, base.bulletSpeed);
        }
    }
}
