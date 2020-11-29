using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpeed : MonoBehaviour
{
    public Sprite speed1;
    public Sprite speed2;
    public Sprite speed3;
    public Text speedText;

    void Start()
    {
    }

    public int speedMode { get; private set; } = 1;
    public void ChangeSpeedMode()
    {
        if (speedMode == 1)
        {
            speedMode = 2;
            Time.timeScale = 2f;
            speedText.text = speedMode.ToString();
            gameObject.GetComponent<Image>().sprite=speed2;
        }
        else if (speedMode == 2)
        {
            speedMode = 3;
            Time.timeScale = 4f;
            speedText.text = speedMode.ToString();
            gameObject.GetComponent<Image>().sprite = speed3;
        }
        else
        {
            speedMode = 1;
            Time.timeScale = 1f;
            speedText.text = speedMode.ToString();
            gameObject.GetComponent<Image>().sprite = speed1;
        }
    }
}
