using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using System.Linq;
using NeuroEvolution;
using Unity.Collections;
using MultiThreading;
using Unity.Jobs;
using System.Threading;

public class PlayerAI : MonoBehaviour
{
    public NeuralNetwork brain;

    private GameObject target;

    [SerializeField]
    private CharacterController characterController;

    public float MovementSpeed;

    public float RotationSpeed = 30f;

    private float[] guess;

    private Population population;

    [SerializeField]
    public int playerNumber;


    [SerializeField]
    private float maxEnergie;
    [SerializeField]
    private float energie;

    [SerializeField]
    private float[] inputInformation;

    [SerializeField]
    private bool hitWall;
    [SerializeField]
    private bool hitTarget;

    [SerializeField]
    private float RayLength;
   
    private bool gameEnd;
    private bool gameOver;

    [SerializeField]
   // private float[] inputInformation;



    // Start is called before the first frame update
    void Start()
    {
        RayLength = 30;
        MovementSpeed = 0.2f;

    }

    //Berekent de fitness op basis van de distance to target die een max value van 2 heeft, de energie die een max value van 1 heeft, 
    //als hij het target geraakt heeft komt er 1 bij door de one line if statement en als de speler een muur heeft geraakt dan komt er niks bij anders 1.
    [SerializeField]
    public float Fitness()
    {
        float fitness = (7.5f - (DistanceToTarget() * 7.5f)) + ((hitTarget) ? 2.5f : 0);

        return fitness;
    }
    

    public void InitializePlayer(Population population, GameObject target, int playerNumber, int maxEnergie,NeuralNetwork brain)
    {
        if(brain == null)
        {
            this.brain = new NeuralNetwork(15, 8, 2);
        }
        else
        {
            this.brain = brain;
        }

        this.target = target;
        this.playerNumber = playerNumber;

        this.maxEnergie = maxEnergie;
        energie = maxEnergie;

        this.population = population;

        RayLength = 5;

        gameOver = false;



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(energie <= 0)
        {
            gameOver = true;
            GameEnd();
        }
        else
        {
            Think();
        }







        //Vector3 movementVector = new Vector3(0, 0, 0.2f);
        //movementVector = transform.TransformDirection(movementVector);

        //characterController.Move(movementVector);

        //if (Input.GetKey(KeyCode.W))
        //{
        //    MovePlayer(1, 0);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    MovePlayer(0, 1);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    MovePlayer(0, -1);
        //}


    }

    public void Think()
    {
        inputInformation = new float[15];

        int offset = -30;
        for (int i = 0; i < 6; i++)
        {
            offset += 10;
            inputInformation[i] = DistanceToWall(offset)[0];
            inputInformation[i + 7] = DistanceToWall(offset)[1];

        }
        inputInformation[14] = DistanceToTarget();
        //inputInformation[8] = RotationLeft();
        //inputInformation[9] = RotationRight();
        //inputInformation[10] = energie / maxEnergie;

        
        float[] outputInformation = brain.FeedForward(inputInformation);

        MakeDecision(outputInformation);
    }

    private void MakeDecision(float[] outputInformation)
    {
        float maxValue = outputInformation.Max();
        float maxIndex = outputInformation.ToList().IndexOf(maxValue);
        if (maxIndex == 0)
        {
            MovePlayer(0, 1);
        }
        else if (maxIndex == 1)
        {
            MovePlayer(0, -1);
        }
        //else if(maxIndex == 2)
        //{
        //    MovePlayer(1, 0);
        //}

    }

    private float RotationLeft()
    {

        if(transform.rotation.y < 0)
        {
            if(transform.rotation.y <= -360)
            {
                return (transform.rotation.y + 360) * -1;
            }
            else
            {
                return transform.rotation.y * -1;
            }
        }
        else
        {
            return 0;
        }        
    }

    private float RotationRight()
    {
        if(transform.position.y > 0)
        {
            if(transform.position.y > 360)
            {
                return transform.rotation.y - 360; 
            }
            else
            {
                return transform.rotation.y;
            }
        }
        else
        {
            return 0;
        }
    }

    private float DistanceToTarget()
    {

        float distanceTarget = Vector3.Distance(target.transform.position, transform.position) / 95;

        return distanceTarget;
    }


    private float[] DistanceToWall(int offset)
    {
       // Vector3 vectorForward = transform.forward;
        var rotate = Quaternion.Euler(0, offset, 0);
        Vector3 vectorAngle = rotate * Vector3.forward;
        Vector3 visualAngle = transform.TransformDirection(vectorAngle) * 30;
        
        float[] rayInputs = new float[2];

        Ray Ray = new Ray(transform.position, visualAngle);

        if (Physics.Raycast(Ray, out RaycastHit hit, RayLength))
        {
            if (hit.collider.tag == "Obstacle")
            {
                rayInputs[0] = hit.distance / 30;

            }
            if(hit.collider.tag == "Finish")
            {
                rayInputs[1] = hit.distance / 30;
            }
        }
        //Debug.DrawRay(transform.position, visualAngle, Color.cyan);

        return rayInputs;
    }

    

 



    //Deze functie beweegt en draaid de speler op basis van de movementAmount en rotationAmount. Als de movementAmount 1 is dan gaat hij vooruit en als hij -1 is dan gaat hij achteruit. 
    //Zo geld dit ook voor de rotationAmount. Als die 1 is dan draait hij naar rechts en als hij -1 is dan draait hij naar links.
    public void MovePlayer(float movementAmount, float rotationAmount)
    {
        if (!gameEnd && !gameOver)
        {
            energie -= 1;

            Vector3 movementVector = transform.TransformDirection(0,0, MovementSpeed);

            characterController.Move(movementVector);

            if (rotationAmount > 0)
            {
                transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
            }
            else if (rotationAmount < 0)
            {
                transform.Rotate(-Vector3.up * RotationSpeed * Time.deltaTime);
            }

            //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + rotationAmount / 50, transform.rotation.z, transform.rotation.w);
        }
    }

    void OnTriggerEnter(Collider collider)
        {

        if (collider.tag == "Obstacle")
        {
            hitWall = true;
            gameOver = true;
            GameEnd();
        }
        if (collider.tag == "Finish")
        {
            print("You have reached the finish");
            hitTarget = true;
            GameEnd();
        }
    }

    void GameEnd()
    {
        gameEnd = true;
        population.PlayerDead(playerNumber);
    }
}
