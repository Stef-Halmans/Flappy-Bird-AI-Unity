using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace NeuroEvolution
{
    public class Population : MonoBehaviour
    {
        private Text generationNumber;
        private Text bestFitness;

        public GameObject SpawnPoint;
        public GameObject Target;

        public GameObject PlayerPrefab;

        public GameObject[] Players;

        public int maxEnergie = 1000;

        public int BestPlayerNumber;

        private float[] fitnesses;

        [SerializeField]
        private bool[] playerAlive;

        [SerializeField]
        private int populationSize;

        [SerializeField]
        private float learningRate;

        private int oldFitness;

        System.Random randomInt;



        // Start is called before the first frame update
        void Start()
        {

            CreatePopulationRandom(populationSize);
            learningRate = 0.1f;
            //  Time.timeScale = 2;
            randomInt = new System.Random();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Parallel.For(0, populationSize, i =>

            //{
            //    Players[i].GetComponent<PlayerAI>().Think();
            //});
//            BestPlayer();
            //Debug.Log(UnityEngine.Random.Range(0, fitnesses.Length));

        }

        public void PlayerDead(int playerNumber)
        {
            playerAlive[playerNumber] = false;
            if (playerAlive.AreAll(x => !x))
            {
                MutatePopulation();
            }

        }



        public void CreatePopulationRandom(int size)
        {
            populationSize = size;
            Players = new GameObject[populationSize];
            playerAlive = new bool[size];

            for (int i = 0; i < populationSize; i++)
            {
                playerAlive[i] = true;
                Players[i] = Instantiate(PlayerPrefab, SpawnPoint.transform);
                Players[i].GetComponent<PlayerAI>().InitializePlayer(this, Target, i, maxEnergie, null);
            }
        }

        public void CreateNewPopulation(NeuralNetwork[] brains)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                Destroy(Players[i]);
            }
            for (int i = 0; i < brains.Length; i++)
            {
                playerAlive[i] = true;
                Players[i] = Instantiate(PlayerPrefab, SpawnPoint.transform);
                Players[i].GetComponent<PlayerAI>().InitializePlayer(this, Target, i, maxEnergie, brains[i]);
            }
        }

        public void MutatePopulation()
        {
            GetFitnesses();
            NeuralNetwork[] brains = new NeuralNetwork[populationSize];
            brains[0] = new NeuralNetwork(Players[MatrixMath.GetIndexHighestNumber(fitnesses)].GetComponent<PlayerAI>().brain);
            for (int i = 0; i < populationSize; i++)
            {
                int index1 = (int)AcceptReject();
                int index2 = (int)AcceptReject();

                //brains[i] = new NeuralNetwork(Players[index1].GetComponent<PlayerAI>().brain, Players[index2].GetComponent<PlayerAI>().brain, learningRate);
            }
            CreateNewPopulation(brains);

        }

        public float[] GetFitnesses()
        {
            fitnesses = new float[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                fitnesses[i] = Players[i].GetComponent<PlayerAI>().Fitness();
            }

            return fitnesses;
        }
        public void BestPlayer()
        {
            GetFitnesses();
            BestPlayerNumber = MatrixMath.GetIndexHighestNumber(fitnesses);

            Players[BestPlayerNumber].GetComponent<Renderer>().material.color = Color.red;
            if (oldFitness != BestPlayerNumber)
            {
                Players[oldFitness].GetComponent<Renderer>().material.color = Color.grey;
            }
            oldFitness = BestPlayerNumber;

        }



        public int? AcceptReject()
        {
            int? index = null; 
            bool loop = true;
            int error = 0;
            while (loop)
            {
                error++;
                if(error > 100)
                {
                    loop = false;
                    print("while loop is infinite");
                }
                index = UnityEngine.Random.Range(0, fitnesses.Length);
                float random = MatrixMath.Random(0, 10);
                float fitness = fitnesses[(int)index];
                if (fitness > random)
                {
                    loop = false;
                }

            }
            return index;
           

        }
    }
}

