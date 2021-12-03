using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeuroEvolution;
using UnityEngine.UI;
using System;

public class ShowMatrix : MonoBehaviour
{
    Population population;
    public Text text;
    //private Text[,] texts;

    public GameObject StartPosition;

    public float[] array;
    public float[] array2;
    public float[] array3;
    public float[] array4;
    public float[] array5;
    public float[] array6;
    public float[] array7;
    public float[] array8;
    public float[] array9;
    

    // Start is called before the first frame update
    void Start()
    {
        population = FindObjectOfType<Population>();

    }

    public void InstantiateMatrix(float[,] matrix, float offset)
    {
        Debug.Log("instantiate matrix");
        Vector3 pos = new Vector3(StartPosition.transform.position.x, StartPosition.transform.position.y - offset, StartPosition.transform.position.z);
        Text[,] texts = new Text[matrix.GetLength(0), matrix.GetLength(1)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            pos = new Vector3(StartPosition.transform.position.x, StartPosition.transform.position.y - i * 42 - offset, pos.z);

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                //print("row "+i + " colm "+ j + " value " + matrix[i, j]);
                Quaternion rotation = new Quaternion(0, 0, 0, 0);
                pos = new Vector3(StartPosition.transform.position.x + j * 42, pos.y, pos.z);

                texts[i, j] = Instantiate(text, pos, rotation, StartPosition.transform);
                texts[i, j].text = Math.Round(matrix[i, j], 2).ToString();
            }
        }
       // texts = null;
    }

    public void ShowArray(float[] array)
    {
        this.array = array;
    }

    // Update is called once per frame

}
