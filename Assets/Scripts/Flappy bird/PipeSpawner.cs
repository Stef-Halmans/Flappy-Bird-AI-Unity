using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float maxTime = 1;
    public float timer = 0;
    public GameObject pipe;
    public float height;
    public GameObject currentPipes;
    public float heightHeightPipe;
    public float heightLowPipe;
    public bool gameStop = false;

    public List<GameObject> pipes;

    // Start is called before the first frame update
    void Start()
    {
        CreatePipe();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStop){
            if (timer > maxTime)
            {
                CreatePipe();
                timer = 0;
            }
            timer += Time.deltaTime;
        }

    }

    public void CreatePipe()
    {

        GameObject newPipe = Instantiate(pipe);
        newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
        currentPipes = newPipe;
        pipes.Add(newPipe);
        heightHeightPipe = newPipe.transform.position.y + newPipe.transform.GetChild(0).transform.position.y;
        heightLowPipe = newPipe.transform.position.y + newPipe.transform.GetChild(1).transform.position.y;
        Destroy(newPipe, 15);
    }

    public void DestroyPipes()
    {
        foreach(GameObject pipe in pipes)
        {
            Destroy(pipe);
        }
    }
}
