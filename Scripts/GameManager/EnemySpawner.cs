using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject enemyGreenPrefab;
    public GameObject enemyRedPrefab;
    public GameObject enemyYellowPrefab;
    public GameObject enemyOrangePrefab;
    public GameObject enemySlowFast;
    public Text waveCountdownText;

    public static EnemySpawner instance;
    float timeBetweenWaves = 10f;
    float countdown = 1f;
    int waveNumber = 0;

    LinkedList<GameObject> enemiesSpawned = new LinkedList<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        /*foreach (int index in Enumerable.Range(1, 1000))
        {
            enemiesSpawned.AddLast(Instantiate(enemyGreenPrefab, new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), Random.Range(0f, 20f)), Quaternion.identity, transform));
        }*/

    }
    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        //waveCountdownText.text = countdown.ToString("0");
    }
    IEnumerator SpawnWave()
    {
        waveNumber++;

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy()
    {
        int random = Random.Range(1, 1);
        if (random == 1)
        {
            if (EnemyRed.enemiesDeathForReusingGameObjectQueue.Count > 0)
            {
                GameObject enemyDequeued = EnemyRed.enemiesDeathForReusingGameObjectQueue.Dequeue();
                enemyDequeued.GetComponent<EnemyFather>().ResetEnemyForSpawn(spawnPoint.position);
                enemyDequeued.SetActive(true);
            }
            else
            {
                enemiesSpawned.AddLast(Instantiate(enemyRedPrefab, spawnPoint.position, spawnPoint.rotation, transform));
            }
        }
        else if (random == 2)
        {
            if (EnemyGreen.enemiesDeathForReusingGameObjectQueue.Count > 0)
            {
                GameObject enemyDequeued = EnemyGreen.enemiesDeathForReusingGameObjectQueue.Dequeue();
                enemyDequeued.GetComponent<EnemyFather>().ResetEnemyForSpawn(spawnPoint.position);
                enemyDequeued.SetActive(true);
            }
            else
            {
                enemiesSpawned.AddLast(Instantiate(enemyGreenPrefab, spawnPoint.position, spawnPoint.rotation, transform));
            }
        }
        else if (random == 3)
        {
            if (EnemyYellow.enemiesDeathForReusingGameObjectQueue.Count > 0)
            {
                GameObject enemyDequeued = EnemyYellow.enemiesDeathForReusingGameObjectQueue.Dequeue();
                enemyDequeued.GetComponent<EnemyFather>().ResetEnemyForSpawn(spawnPoint.position);
                enemyDequeued.SetActive(true);
            }
            else
            {
                enemiesSpawned.AddLast(Instantiate(enemyYellowPrefab, spawnPoint.position, spawnPoint.rotation, transform));
            }
        }
        else if (random == 4)
        {
            if (EnemyOrange.enemiesDeathForReusingGameObjectQueue.Count > 0)
            {
                GameObject enemyDequeued = EnemyOrange.enemiesDeathForReusingGameObjectQueue.Dequeue();
                enemyDequeued.GetComponent<EnemyFather>().ResetEnemyForSpawn(spawnPoint.position);
                enemyDequeued.SetActive(true);
            }
            else
            {
                enemiesSpawned.AddLast(Instantiate(enemyOrangePrefab, spawnPoint.position, spawnPoint.rotation, transform));
            }
        }
        else if (random == 5)
        {
            if (EnemySlowFast.enemiesDeathForReusingGameObjectQueue.Count > 0)
            {
                GameObject enemyDequeued = EnemySlowFast.enemiesDeathForReusingGameObjectQueue.Dequeue();
                enemyDequeued.GetComponent<EnemyFather>().ResetEnemyForSpawn(spawnPoint.position);
                enemyDequeued.SetActive(true);
            }
            else
            {
                enemiesSpawned.AddLast(Instantiate(enemySlowFast, spawnPoint.position, spawnPoint.rotation, transform));
            }
        }
    }

    public virtual void RemoveEnemy(GameObject t)
    {
        enemiesSpawned.Remove(t);
    }
}
