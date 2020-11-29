using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class TowerFather : MonoBehaviour
{
    public readonly static int enemyLayer = 8;
    public readonly static int layerMask = 1 << enemyLayer;

    public bool towerPlaced = false;
    //parametros de la torre
    public float range { get; private set; }
    protected float atkSpeed { get; private set; }
    protected float bulletSpeed { get; private set; }
    public float damage { get; private set; }
    public int towerCost { get; private set; }

    float tiempoEntreBusquedasSiBuscamosUnaVezYNoEncontramosEnemigo = 0.2f;

    public Queue<GameObject> bulletQueue = new Queue<GameObject>();
    protected GameObject nearestEnemy { get; private set; } = null;
    protected float rangePow;
    protected float lastAtk = 0.0f;
    /*attackMode(first, first y mantener hasta que se salga, last, strongest, weakest, random, air, ground)
	attackType(normal, slow, freeze, venom, amplify damage...)
		model
		fireSound
		projectil
		price
		fireRate
		range
		damage
		upgrade
		*/
    public TowerFather(float damage, float range, float atkSpeed, float bulletSpeed, int towerCost)
    {
        this.damage = damage;
        this.range = range;
        this.atkSpeed = atkSpeed;
        this.bulletSpeed = bulletSpeed;
        this.towerCost = towerCost;
        rangePow = range * range;
    }

    //probar si cambia al llamar a el metodo GetEnemiesInRange en vez de repetir el codigo en cada uno
    //probar con los diferentes ChooseEnemy
    //ver si entre el random y entre el ChooseStrongest cambian los fps, si no es que esto no influye mucho
    //filtrar antes por transform.position o algo asi
    //cachear los scripts en una tabla hash para no hacer getComponent
    //probar a restar y sacar la m inima en vez del overlapsphere
    //si el BestChoosePrimero es el mejor, para el ChooseStrongest usar uno parecido

    public static float time;

    #region Methods to override

    public abstract void Awake();
    public abstract void EnableRangeSprite(bool enable);
    public abstract void Shoot();
    #endregion

    public virtual void Update()
    {
        if (towerPlaced)
        {
            FindEnemy();
        }
    }

    public virtual void FindEnemy()
    {
        if (nearestEnemy && !nearestEnemy.activeSelf)
            nearestEnemy = null;
        //si ha pasado el tiempo para el siguiente ataque
        lastAtk -= Time.deltaTime;
        if (lastAtk <= 0)
        {
            //si nuestro enemigo ha muerto o no esta en el rango cogemos otro
            if (!nearestEnemy || !nearestEnemy.activeSelf || (transform.position - nearestEnemy.transform.position).sqrMagnitude > rangePow)
                nearestEnemy = BestChooseFirst();

            //si es null, nos  esperamos 0.1 para volver a buscar enemigo
            if (nearestEnemy && nearestEnemy.activeSelf)
            {
                lastAtk = atkSpeed;
                Shoot();
            }
            else
            {
                lastAtk = tiempoEntreBusquedasSiBuscamosUnaVezYNoEncontramosEnemigo;
            }
        }
    }

    protected GameObject BestChooseFirst()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, range, layerMask);
        Collider devolver = null;
        //primero max y luego usarlo o ir usandolo y si conseguimos un nuevo max pues cambiarlo
        int maxWaypoint = 0;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < collidersInRange.Length; i++)
        {
            int way = collidersInRange[i].gameObject.GetComponent<EnemyFather>().nextWayPointIndex;

            if (way > maxWaypoint)
            {
                maxWaypoint = way;
                minDistance = (Waypoints.waypoints[maxWaypoint] - collidersInRange[i].transform.position).sqrMagnitude;
                devolver = collidersInRange[i];
            }
            else if (way == maxWaypoint)
            {
                float distance = (Waypoints.waypoints[maxWaypoint] - collidersInRange[i].transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    devolver = collidersInRange[i];
                }
            }
        }

        if (devolver)
            return devolver.gameObject;
        else
            return null;
    }


    //Coge los que estan en rango, saca el maximo wayPoint por el que van, selecciona solo los que van a ese y elige el que este mas cerca
    protected GameObject ChooseFirst()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, range, layerMask);
        if (collidersInRange.Length == 0)
            return null;
        int maxWaypoint = collidersInRange.Max(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex);
        return OrderAux.MinElement(collidersInRange.Where(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex == maxWaypoint), x => (Waypoints.waypoints[maxWaypoint] - x.transform.position).sqrMagnitude).gameObject;
    }

    protected GameObject ChooseLastEnemy()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, range, layerMask);
        if (collidersInRange.Length == 0)
            return null;
        int minWaypoint = collidersInRange.Min(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex);
        return OrderAux.MaxElement(collidersInRange.Where(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex == minWaypoint), x => (Waypoints.waypoints[minWaypoint] - x.transform.position).sqrMagnitude).gameObject;
    }

    protected GameObject ChooseRandomEnemy()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, range, layerMask);
        if (collidersInRange.Length == 0)
            return null;
        else
            return collidersInRange[0].gameObject;
    }
    //Puede ser que el primero no tenga maxHp, debemos coger la lista de los cercanos, sacar maHp y coger el primero con maxHp
    protected GameObject ChooseStrongestEnemy()
    {
        IEnumerable<Collider> collidersInRange = Physics.OverlapSphere(transform.position, range, layerMask);
        if (collidersInRange.Count() == 0)
            return null;
        float maxHpEnemy = collidersInRange.Max(x => x.gameObject.GetComponent<EnemyFather>().maxHp);
        collidersInRange = collidersInRange.Where(x => x.gameObject.GetComponent<EnemyFather>().maxHp == maxHpEnemy);
        int maxWaypoint = collidersInRange.Max(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex);
        return OrderAux.MinElement(collidersInRange.Where(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex == maxWaypoint), x => (Waypoints.waypoints[maxWaypoint] - x.transform.position).sqrMagnitude).gameObject;
    }

    protected GameObject ChooseWeakestEnemy()
    {
        IEnumerable<Collider> collidersInRange = Physics.OverlapSphere(transform.position, range, layerMask);
        if (collidersInRange.Count() == 0)
            return null;
        float maxHpEnemy = collidersInRange.Min(x => x.gameObject.GetComponent<EnemyFather>().maxHp);
        collidersInRange = collidersInRange.Where(x => x.gameObject.GetComponent<EnemyFather>().maxHp == maxHpEnemy);
        int maxWaypoint = collidersInRange.Max(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex);
        return OrderAux.MinElement(collidersInRange.Where(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex == maxWaypoint), x => (Waypoints.waypoints[maxWaypoint] - x.transform.position).sqrMagnitude).gameObject;
    }
    protected GameObject ChooseNearestEnemy()
    {
        Collider hitCollider = OrderAux.MinElement(Physics.OverlapSphere(transform.position, range, layerMask), x => (transform.position - x.transform.position).sqrMagnitude);

        if (hitCollider)
            return hitCollider.gameObject;
        else
            return null;
    }
    protected Collider[] ReturnAllNearestEnemies()
    {
        return Physics.OverlapSphere(transform.position, range, layerMask);
    }

    public void Enqueue(GameObject bullet)
    {
        bulletQueue.Enqueue(bullet);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
