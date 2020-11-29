using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSpawner : MonoBehaviour
{
    public GameObject errorPrefab;
    public GameObject prefabTower1;
    public GameObject prefabTower2;
    public GameObject prefabTower3;
    public GameObject prefabTower4;
    public GameObject prefabTower5;
    public GameObject prefabTower6;
    public GameObject prefabTower7;
    public GameObject prefabTower8;
    public GameObject prefabTower9;
    public GameObject prefabTower10;

    public static TurretSpawner instance;
    GameObject selectedTower1;
    GameObject selectedTower2;
    GameObject selectedTower3;
    GameObject selectedTower4;
    GameObject selectedTower5;

    public List<GameObject> turretsSpawned = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        selectedTower1 = TowerGameObjectSelector(GameManager.choosenTurret1);
        selectedTower2 = TowerGameObjectSelector(GameManager.choosenTurret2);
        selectedTower3 = TowerGameObjectSelector(GameManager.choosenTurret3);
        selectedTower4 = TowerGameObjectSelector(GameManager.choosenTurret4);
        selectedTower5 = TowerGameObjectSelector(GameManager.choosenTurret5);
    }
    //de todas las torres, dependiendo de las ids de las 4 torres seleccionadas asignamos los GameObject de las torres que sean
    GameObject TowerGameObjectSelector(int towerId)
    {
        switch(towerId){
            case 1:
                return prefabTower1;
            case 2:
                return prefabTower2;
            case 3:
                return prefabTower3;
            case 4:
                return prefabTower4;
            case 5:
                return prefabTower5;
            case 6:
                return prefabTower6;
            case 7:
                return prefabTower7;
            case 8:
                return prefabTower8;
            case 9:
                return prefabTower9;
            case 10:
                return prefabTower10;
            default:
                return errorPrefab;
        }
    }
 
    public void SpawnSelectedTurret1()
    {
        Instantiate(selectedTower1, transform.position, Quaternion.identity);
    }
    public void SpawnSelectedTurret2()
    {
        Instantiate(selectedTower2, transform.position, Quaternion.identity);
    }
    public void SpawnSelectedTurret3()
    {
        Instantiate(selectedTower3, transform.position, Quaternion.identity);
    }
    public void SpawnSelectedTurret4()
    {
        Instantiate(selectedTower4, transform.position, Quaternion.identity);
    }
    public void SpawnSelectedTurret5()
    {
        Instantiate(selectedTower5, transform.position, Quaternion.identity);
    }
}
