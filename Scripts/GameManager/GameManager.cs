using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text goldText;
    public Text speedText;
    public static GameManager instance;
    
    public static int choosenTurret1 = 3;
    public static int choosenTurret2 = 4;
    public static int choosenTurret3 = 5;
    public static int choosenTurret4 = 6;
    public static int choosenTurret5 = 1;

    public static int choosenHeroe = 1;
    public int speedMode { get; private set; } = 1;
    

    [SerializeField]
    int gold = 1000;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        goldText.text = gold.ToString();
        speedText.text = speedMode.ToString();
    }
    /*public void ChangeSpeedMode()
    {
        if (speedMode == 1)
        {
            speedMode = 2;
            Time.timeScale = 2f;
            speedText.text = speedMode.ToString();
        }
        else if (speedMode == 2)
        {
            speedMode = 3;
            Time.timeScale = 4f;
            speedText.text = speedMode.ToString();
        }
        else
        {
            speedMode = 1;
            Time.timeScale = 1f;
            speedText.text = speedMode.ToString();
        }
    }*/
    private void Update()
    {
    }
    //heroe elegido
    //torres elegidas

    public void AddGold(int receivedGold)
    {
        gold += receivedGold;
        goldText.text = gold.ToString();
    }
    public void RemoveGold(int goldCost)
    {
        gold -= goldCost;
        goldText.text = gold.ToString();
    }
    public bool PlaceTower(int goldCost)
    {
        if (gold >= goldCost)
        {
            RemoveGold(goldCost);
            return true;
        }
        else
        {
            return false;
        }
    }
}
