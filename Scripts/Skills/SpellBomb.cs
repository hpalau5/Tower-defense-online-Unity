using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBomb : SpellFather
{
    public SpellBomb() : base(50f,10f,5f,1f)
    {
    }
    public override void SetTargetPosition (Vector3 pos)
    {
        destinationPosition = pos;
    }

    public override void Update()
    {
        if (destinationPosition != Vector3.zero)
        {
            transform.position += (destinationPosition - this.gameObject.transform.position).normalized * speed * Time.deltaTime;
        }
        else
        {
            //DestroyBullet();
        }
    }
    public void Awake()
    {
        Destroy(gameObject,10f);
    }
}
