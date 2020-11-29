using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLighting : BulletFather
{
    public BulletLighting() : base()
    {
    }
    //la bala es instant, se hace un rayo desde la torre al enemigo
    //deberia coger al enemigo mas cercano y si esta a menos de x rango saltar a ese, asi x veces
}

