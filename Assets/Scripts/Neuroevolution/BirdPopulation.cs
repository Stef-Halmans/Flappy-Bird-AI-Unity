using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BirdPopulation : MonoBehaviour
{
    public Text GenerationNumberText;
    public Text BestFitness;

    private int generationNumber;

    public GameObject SpawnPoint;
    public GameObject Target;

    public GameObject BirdPrefab;

    public GameObject[] birds;

    public int maxEnergie = 1000;

    public int BestPlayerNumber;

    [SerializeField]
    private float[] fitnesses;

    [SerializeField]
    private bool[] birdsAlive;

    [SerializeField]
    private int populationSize;

    [SerializeField]
    private float learningRate = 0.1f;

    private int oldFitness;

    System.Random randomInt;

    [SerializeField]
    private PipeSpawner pipeSpawner;

    private BirdAI[] birdAIs;

    public int HighScore;
    public int Score;
    public Text HighscoreText;
    public Text ScoreText;


    void Start()
    {
        CreatePopupolationRandom(populationSize);
        Time.timeScale = 2;
        generationNumber = 1;
        Score = 0;
        HighScore = 0;

        HighscoreText.text = "Highscore = " + HighscoreText.ToString();
        ScoreText.text = "Current score = " + Score.ToString();
        GenerationNumberText.text = "Generation Number = " + generationNumber.ToString();

    }

    // Update is called once per frame
    void Update()
    {
     //   BestPlayer();
        //Vector2.Distance()
    }

    public void CreatePopupolationRandom(int size)
    {

        populationSize = size;
        birds = new GameObject[size];
        birdsAlive = new bool[size];
        birdAIs = new BirdAI[size];

        for (int i = 0; i < populationSize; i++)
        {
            birdsAlive[i] = true;
            birds[i] = Instantiate(BirdPrefab, SpawnPoint.transform);
            birdAIs[i] = birds[i].GetComponent<BirdAI>();
            birdAIs[i].CreateBird(this, pipeSpawner, i, null);
        }
    }

    public void BirdDead(int playerNumber)
    {
        birdsAlive[playerNumber] = false;
        if (birdsAlive.AreAll(x => !x))
        {
            MutatePopulation();

        }
    }

    public void MutatePopulation()
    {
        pipeSpawner.gameStop = true;
        CaltulateFitnesses();
        NeuralNetwork[] brains = new NeuralNetwork[populationSize];
        brains[0] = new NeuralNetwork(birdAIs[MatrixMath.GetIndexHighestNumber(fitnesses)].brain);

        for (int i = 0; i < populationSize; i++)
        {
            int index1 = ChooseBird();
            //int index2 = ChooseBird();
            brains[i] = new NeuralNetwork(birdAIs[index1].brain, learningRate);//, birdAIs[index2].brain, learningRate);
        }

        CreateNewPopulation(brains);
    }
    public void CreateNewPopulation(NeuralNetwork[] brains)
    {
        for (int i = 0; i < birds.Length; i++)
        {
            Destroy(birds[i]);
        }
        birds = new GameObject[brains.Length];
        birdAIs = new BirdAI[brains.Length];
        pipeSpawner.DestroyPipes();


        for (int i = 0; i < brains.Length; i++)
        {
            birdsAlive[i] = true;
            birds[i] = Instantiate(BirdPrefab, SpawnPoint.transform);
            birdAIs[i] = birds[i].GetComponent<BirdAI>();
            birdAIs[i].CreateBird(this, pipeSpawner, i, brains[i]);


        }
        pipeSpawner.CreatePipe();
        pipeSpawner.timer = 0;
        pipeSpawner.gameStop = false;
        generationNumber += 1;
        GenerationNumberText.text = "Generation number = " + generationNumber.ToString();
        if (Score > HighScore)
        {
            HighScore = Score;
        }
        HighscoreText.text = "Highscore = " + HighScore.ToString();
        Score = 0;
    }

    public void StartoverButton()
    {
        for (int i = 0; i < birds.Length; i++)
        {
            Destroy(birds[i]);
        }

        pipeSpawner.DestroyPipes();
        
        CreatePopupolationRandom(populationSize);
        
        pipeSpawner.CreatePipe();
        pipeSpawner.timer = 0;
        pipeSpawner.gameStop = false;
        
        generationNumber = 0;
        Score = 0;
        HighScore = 0;

        HighscoreText.text = "Highscore = " + HighScore.ToString();
        ScoreText.text = "Current score = " + Score.ToString();
        GenerationNumberText.text = "Generation Number = " + generationNumber.ToString();
    }

    public void CaltulateFitnesses()
    {
        fitnesses = new float[birdAIs.Length];
        float sum = 0;
        for (int i = 0; i < birdAIs.Length; i++)
        {
            sum += birdAIs[i].score;
        }
        for (int i = 0; i < birdAIs.Length; i++)
        {
            float fitness = birdAIs[i].score / sum;

            birdAIs[i].fitness = fitness;

            fitnesses[i] = fitness;

        }

    }
    public void BestPlayer()
    {
        CaltulateFitnesses();
        BestPlayerNumber = MatrixMath.GetIndexHighestNumber(fitnesses);

        birds[BestPlayerNumber].GetComponent<Renderer>().material.color = Color.red;
        if (oldFitness != BestPlayerNumber)
        {
            birds[oldFitness].GetComponent<Renderer>().material.color = Color.grey;
        }
        oldFitness = BestPlayerNumber;

    }

    public int ChooseBird()
    {
        int index = 0;
        float r = MatrixMath.Random(0, 1);

        while (r > 0)
        {
            r = r - birdAIs[index].fitness;
            index++;
        }
        index--;

        return index;
    }

}


