using AI;
using NeuroEvolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAI : MonoBehaviour
{
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float up;
    [SerializeField]
    private float gravity;

    public float score;
    public float fitness;

    public int playerNumber;

    private int ticks;

    private bool gameEnd = false;
    private bool startGame = false;

    public NeuralNetwork brain;
    private BirdPopulation birdPopulation;
    private PipeSpawner pipeSpawner;

    [SerializeField]
    private float[] inputInformation;
    public int OwnScore;
  

    void Start()
    {
        OwnScore = 0;
    }

    public void CreateBird(BirdPopulation birdPopulation, PipeSpawner pipeSpawner, int playerNumber, NeuralNetwork brain)
    {
        if(brain == null)
        {
            this.brain = new NeuralNetwork(4, 4, 1);
        }
        else
        {
            this.brain = brain;
        }

        this.birdPopulation = birdPopulation;

        this.pipeSpawner = pipeSpawner;

        startGame = true;

        this.playerNumber = playerNumber;

    }


    void Update()
    {

        if (startGame && !gameEnd)
        {
            score++;
            Think();
            ticks++;

            if (Input.GetMouseButtonDown(0))
            {

            }
            if (ticks % 2 == 0)
            {
                velocity += gravity;
            }
            if (transform.position.y >= 1.2)
            {
                velocity = 0 + gravity;
            }
            transform.position = new Vector2(transform.position.x, transform.position.y + velocity);
        }
        

    }

    private void Think()
    {
        if (pipeSpawner.currentPipes == null) return;
        inputInformation = new float[brain.inputNodes];
   
        inputInformation[0] = transform.position.y;
        if(pipeSpawner.currentPipes.transform.position.x - this.transform.position.x >= 0)
        {
            inputInformation[1] = pipeSpawner.currentPipes.transform.position.x - this.transform.position.x;
        }
        else
        {
            inputInformation[1] = 0;
        }
        inputInformation[2] = pipeSpawner.heightHeightPipe;
        inputInformation[3] = pipeSpawner.heightLowPipe;

        float[] output = new float[brain.outputNodes];

        output = brain.FeedForward(inputInformation);
        if(output[0] > 0.5f)
        {
            velocity = up;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Ground" || collider.tag == "Obstacle")
        {
            gameEnd = true;
            gameObject.GetComponent<Renderer>().enabled = false;
            birdPopulation.BirdDead(playerNumber);
        }
        if (collider.tag == "Pipe")
        {
            OwnScore++;
            if(OwnScore > birdPopulation.Score)
            {
                birdPopulation.Score = OwnScore;
                birdPopulation.ScoreText.text = "Current score = " + birdPopulation.Score.ToString();

                if (birdPopulation.Score > birdPopulation.HighScore)
                {
                    birdPopulation.HighScore = birdPopulation.Score;
                    birdPopulation.HighscoreText.text = "Highscore = " + birdPopulation.HighScore.ToString();
                }
            }

        }

    }
}
