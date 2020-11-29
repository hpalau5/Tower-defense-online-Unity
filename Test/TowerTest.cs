using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerTest : MonoBehaviour
{
    public GameObject enemyPrefab;
    List<GameObject> enemiesSpawned = new List<GameObject>();
    float range = 100f;
    int layer = 8;
    public static TowerTest instance;

    void Awake()
    {
        instance = this;
    }
    void Test()
    {
        foreach (int index in Enumerable.Range(1, 1000))
        {
            enemiesSpawned.Add(Instantiate(enemyPrefab, new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), Random.Range(0f, 20f)), Quaternion.identity));
            //enemiesSpawned.Add(Instantiate(enemyPrefab, new Vector3(index, index, index), Quaternion.identity));
        }
        float time;

        time = Time.realtimeSinceStartup;
        foreach (int index in Enumerable.Range(1, 1000))
        {
            ChooseNearestEnemyNew4();
        }
        Debug.Log(Time.realtimeSinceStartup - time);
    }

    void Start()
    {
        Test();
    }
    protected GameObject ChooseNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        float distanceToEnemy;

        foreach (GameObject enemy in enemiesSpawned)
        {
            float x = transform.position.x - enemy.transform.position.x;
            float z = transform.position.z - enemy.transform.position.z;
            //saca la distancia sin hacer la raiz cuadrada, para compararlas no hace falta sacar la raiz cuadrada
            distanceToEnemy = x * x + z * z;
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
    protected GameObject BestColliderChooseNearestEnemy(float range, int layer)
    {
        int layerMask = 1 << layer;
        Collider hitCollider = OrderAux.MinElement(Physics.OverlapSphere(transform.position, range, layerMask).Where(x => x.CompareTag("enemy")), x => (transform.position - x.transform.position).sqrMagnitude);
        if (hitCollider)
            return hitCollider.gameObject;
        else
            return null;
    }
    Collider ColliderChooseNearestEnemy2(float range, int layer)
    {
        int layerMask = 1 << layer;
        IEnumerable<Collider> hitColliders = Physics.OverlapSphere(transform.position, range, layerMask).Where(x => x.CompareTag("enemy"));
        float max = hitColliders.Max(x => (transform.position - x.transform.position).sqrMagnitude);
        return hitColliders.First(x => (transform.position - x.transform.position).sqrMagnitude == max);
    }
    Collider ColliderChooseNearestEnemy3(float range, int layer)
    {
        int layerMask = 1 << layer;
        Collider collider = Physics.OverlapSphere(transform.position, range, layerMask).Where(x => x.CompareTag("enemy")).
            OrderBy(x => (transform.position - x.transform.position).sqrMagnitude).First();
        return collider;
    }

    Collider2D BestColliderChooseNearestEnemy2(float range, int layer)
    {

        int layerMask = 1 << layer;
        Collider2D collider = Physics2D.OverlapCircle(new Vector2(10, 10), range);
        return collider;
    }
    //--------------------------------------------------------------------------------

    /*Primero filtra los que estan en un cuadrado y luego saca el mas cercano de ese cuadrado
	*/
    protected GameObject ChooseNearestEnemyNew1()
    {
        GameObject nearestEnemy = null;
        float minX = transform.position.x - range;
        float minZ = transform.position.z - range;
        float maxX = transform.position.x + range;
        float maxZ = transform.position.z + range;

        nearestEnemy= OrderAux.MaxByGameObject(enemiesSpawned.Where(x => x.transform.position.x > minX && x.transform.position.z > minZ &&
        x.transform.position.x < maxX && x.transform.position.z < maxZ), x => (transform.position - x.transform.position).sqrMagnitude);
        return nearestEnemy;
    }

    /*Ademas ahora usa int para filtrar a ver si es mas rapido que con floats, la pega es que deja mas numeros en la lista*/
    protected GameObject ChooseNearestEnemyNew3()
    {
        GameObject nearestEnemy = null;
        int minX = (int)(transform.position.x - range);
        int minZ = (int)(transform.position.z - range);
        int maxX = (int)(transform.position.x + range);
        int maxZ = (int)(transform.position.z + range);

        nearestEnemy = OrderAux.MaxByGameObject(enemiesSpawned.Where(x => x.transform.position.x > minX && x.transform.position.z > minZ &&
        x.transform.position.x < maxX && x.transform.position.z < maxZ), x => (transform.position - x.transform.position).sqrMagnitude);
        return nearestEnemy;
    }
    /*Ahora coge los que vayan por el waypoint mas lejano, esten en el cuadrado de la torre y saca la distancia con el siguiente waypoint 
    Este mira el area en un cuadrado, puede pegarle a uno que este fuera del circulo pero en el cuadrado
    */
    protected GameObject ChooseNearestEnemyNew4()
    {
        GameObject nearestEnemy = null;
        int minX = (int)(transform.position.x - range);
        int minZ = (int)(transform.position.z - range);
        int maxX = (int)(transform.position.x + range);
        int maxZ = (int)(transform.position.z + range);

        int maxWaypoint = enemiesNearTower.Max(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex);

        nearestEnemy = OrderAux.MaxByGameObject(enemiesSpawned.Where(x => maxWaypoint == x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex && x.transform.position.x > minX && x.transform.position.z > minZ &&
        x.transform.position.x < maxX && x.transform.position.z < maxZ), x => (Waypoints.waypoints[maxWaypoint] - x.transform.position).sqrMagnitude);
        if (nearestEnemy) return nearestEnemy.gameObject;
        else return null;
    }

    /*Ahora coge los que vayan por el waypoint mas lejano y saca la distancia con el siguiente waypoint */
    protected GameObject Mejor1()
    {
        GameObject nearestEnemy = null;
        int minX = (int)(transform.position.x - range);
        int minZ = (int)(transform.position.z - range);
        int maxX = (int)(transform.position.x + range);
        int maxZ = (int)(transform.position.z + range);
        float rangePow = range * range;
        int maxWaypoint = enemiesNearTower.Max(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex);

        nearestEnemy = OrderAux.MaxByGameObject(enemiesSpawned.Where(x => maxWaypoint == x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex && x.transform.position.x > minX && x.transform.position.z > minZ &&
        x.transform.position.x < maxX && x.transform.position.z < maxZ && (transform.position - x.transform.position).sqrMagnitude < rangePow), x => (Waypoints.waypoints[maxWaypoint] - x.transform.position).sqrMagnitude);
        if (nearestEnemy) return nearestEnemy.gameObject;
        else return null;
    }
    //Lo mismo pero sin hacer el cuadrado antes para sacar distancia de solo los que estan dentro
    protected GameObject ChooseNearestEnemyNew6()
    {
        GameObject nearestEnemy = null;
        float rangePow = range * range;
        int maxWaypoint = enemiesNearTower.Max(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex);

        nearestEnemy = OrderAux.MaxByGameObject(enemiesSpawned.Where(x => maxWaypoint == x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex && (transform.position - x.transform.position).sqrMagnitude < rangePow), x => (Waypoints.waypoints[maxWaypoint] - x.transform.position).sqrMagnitude);
        if (nearestEnemy) return nearestEnemy.gameObject;
        else return null;
    }
    protected GameObject Mejor2()
    {
        int layerMask = 1 << layer;
        int maxWaypoint = enemiesNearTower.Max(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex);
        Collider nearestEnemy = OrderAux.MinElement(Physics.OverlapSphere(transform.position, range, layerMask).Where(x => x.CompareTag("enemy")), x => (Waypoints.waypoints[maxWaypoint] - x.transform.position).sqrMagnitude);
        if (nearestEnemy) return nearestEnemy.gameObject;
        else return null;
    }
    //Lo mismo pero con box en vez de sphere
   /* protected GameObject Mejor3()
    {
        int layerMask = 1 << layer;

        GameObject nearestEnemy = OrderAux.MaxByGameObject(Physics.OverlapBox(transform.position, range, layerMask).Where(x => x.CompareTag("enemy")), x => (Waypoints.wapoints[maxWaypoint] - x.transform.position).sqrMagnitude);
        return nearestEnemy;
    }
   */
    //probar esto pero sin tag, con capas que solo colisione con esos, en la prueba puede ser sin tag y ya
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            enemiesNearTower.AddLast(other.gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            enemiesNearTower.Remove(other.gameObject);
        }
    }
    //cambiar los father para que al morir llamen aqui
    public void EnemyDie(GameObject gameObj)
    {	//descomentarlo al probar los metodos de abajo y crear un collider en la torre
        //enemiesNearTower.Remove(gameObj.transform);
    }
    //para add y remove, linkedList deberia ser mejor
    LinkedList<GameObject> enemiesNearTower = new LinkedList<GameObject>();
    //TriggerCollider por cada torre en la que se guardan los que entran y los que van saliendo
    //Al atacar buscar en esa lista el mas cercano al nexo
    protected GameObject Mejor4()
    {
        GameObject nearestEnemy = null;
        int maxWaypoint = enemiesNearTower.Max(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex);
        nearestEnemy = OrderAux.MaxByGameObject(enemiesNearTower, x => (Waypoints.waypoints[maxWaypoint] - x.transform.position).sqrMagnitude);
        if (nearestEnemy) return nearestEnemy.gameObject;
        else return null;
    }
    //lo mismo pero ahora solo mira entre los que esten en el ultimo waypoint
    //Con mas waypoints deberia ir mejor
    //Hace where y solo usa el maxMy para coger el mayor
    protected GameObject Mejor5Punto1()
    {
        GameObject nearestEnemy = null;
        int maxWaypoint = enemiesNearTower.Max(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex);
        nearestEnemy = OrderAux.MaxByGameObject(enemiesNearTower.Where(x => x.gameObject.GetComponent<EnemyFather>().nextWayPointIndex == maxWaypoint), x => (Waypoints.waypoints[maxWaypoint] - x.transform.position).sqrMagnitude);
        return nearestEnemy;
    }
    //A mas waypoints mejor deberia ir porque descartara a la mayoria en maxWaypoint == x.maxWaypoint
    //Comparar con bastantes waypoints los MejorX, ver si es mejor usar int pero con una lista mayor o float con una lista menor

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}