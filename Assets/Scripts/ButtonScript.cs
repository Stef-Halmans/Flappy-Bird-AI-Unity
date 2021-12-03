using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using AI;
using NeuroEvolution;

public class ButtonScript : MonoBehaviour
{
    Population population;
    public float Speed = 1;
    private int playSpeed;
    public Slider SpeedSlider;
    public Text SpeedText;
 

    public void Start()
    {
        SetRatio(9, 16);
        population = FindObjectOfType<Population>();
        playSpeed = 1;

    }

    void SetRatio(float w, float h)
    {
        if ((((float)Screen.width) / ((float)Screen.height)) > w / h)
        {
            Screen.SetResolution((int)(((float)Screen.height) * (w / h)), Screen.height, true);
        }
        else
        {
            Screen.SetResolution(Screen.width, (int)(((float)Screen.width) * (h / w)), true);
        }
    }

    public void SliderSpeed(float newSpeed)
    {
        Speed = newSpeed;
        Time.timeScale = Speed;
        SpeedText.text = "Speed: " + Speed.ToString("n2") + "x";
    }
    
    
    public void Pause()
    { 
        if (Time.timeScale == 0)
        {
            Time.timeScale = 2;
            //if (playSpeed == 1)
            //{
            //    Time.timeScale = 1;
            //}
            //else if (playSpeed == 2)
            //{
            //    Time.timeScale = 2;
            //}
        }
        else
        {
            Time.timeScale = 0;
        }

    }

    public void Exit()
    {
        Application.Quit();
    }



}
