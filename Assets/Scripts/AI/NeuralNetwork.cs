using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


namespace AI
{
    public class NeuralNetwork
    {
        public int inputNodes;
        public int hiddenNodes;
        public int outputNodes;

        public float[,] inputHiddenWeights;
        public float[,] hiddenOutputWeights;

        public float[] hiddenBiases;
        public float[] outputBiases;

        public NeuralNetwork(NeuralNetwork neuralNetwork)
        {
            inputNodes = neuralNetwork.inputNodes;
            hiddenNodes = neuralNetwork.hiddenNodes;
            outputNodes = neuralNetwork.outputNodes;

            inputHiddenWeights = neuralNetwork.inputHiddenWeights;
            hiddenOutputWeights = neuralNetwork.hiddenOutputWeights;

            hiddenBiases = neuralNetwork.hiddenBiases;
            outputBiases = neuralNetwork.outputBiases;

            
        }

        public NeuralNetwork(NeuralNetwork brain1, float learningRate)
        {
            inputNodes = brain1.inputNodes;
            hiddenNodes = brain1.hiddenNodes;
            outputNodes = brain1.outputNodes;

            inputHiddenWeights = brain1.inputHiddenWeights;
            hiddenOutputWeights = brain1.hiddenOutputWeights;

            hiddenBiases = brain1.hiddenBiases;

            outputBiases = brain1.outputBiases;

            Mutate(learningRate);
            
        }

        public NeuralNetwork(int inputNodes, int hiddenNodes, int outputNodes)
        {
            //maak een nieuw neuralnetwork aan

            this.inputNodes = inputNodes;
            this.hiddenNodes = hiddenNodes;
            this.outputNodes = outputNodes;

            inputHiddenWeights = new float[hiddenNodes, inputNodes];
            hiddenOutputWeights = new float[outputNodes, hiddenNodes];

            hiddenBiases = new float[hiddenNodes];
            outputBiases = new float[outputNodes];
            
            inputHiddenWeights = MatrixMath.Randomize(hiddenNodes, inputNodes, -1, 1);
            hiddenOutputWeights = MatrixMath.Randomize(outputNodes, hiddenNodes, -1, 1);

            hiddenBiases = MatrixMath.Randomize(hiddenNodes, -1, 1);
            outputBiases = MatrixMath.Randomize(outputNodes, -1, 1);


        }

        public void Crossover(NeuralNetwork brain1, NeuralNetwork brain2)
        {
            hiddenBiases = MatrixMath.Combine(brain1.hiddenBiases, brain2.hiddenBiases);
            outputBiases = MatrixMath.Combine(brain1.outputBiases, brain2.outputBiases);

            inputHiddenWeights = MatrixMath.Combine(brain1.inputHiddenWeights, brain2.inputHiddenWeights);
            hiddenOutputWeights = MatrixMath.Combine(brain1.hiddenOutputWeights, brain2.hiddenOutputWeights);
        }

        public void Mutate(float learningRate)
        {
             
            hiddenBiases = MatrixMath.MutateRandom(hiddenBiases, learningRate);
            outputBiases = MatrixMath.MutateRandom(outputBiases, learningRate);

            inputHiddenWeights = MatrixMath.MutateRandom(inputHiddenWeights, learningRate);
            hiddenOutputWeights = MatrixMath.MutateRandom(hiddenOutputWeights, learningRate);
        }

        
        

        //bereken de output van het neuralnetwork
        public float[] FeedForward(float[] input)
        {   
            float[] output = new float[outputNodes];
            float[] hidden = new float[hiddenNodes];
            
            if (input.Length != inputNodes) return output;


            hidden = MatrixMath.MatrixProduct(inputHiddenWeights, input);

            hidden = MatrixMath.Add(hidden, hiddenBiases);

            //activation function!
            hidden = MatrixMath.Sigmoid(hidden);

            output = MatrixMath.MatrixProduct(hiddenOutputWeights, hidden);

            output = MatrixMath.Add(output, outputBiases);
            output = MatrixMath.Sigmoid(output);

            return output;
        }
    }
}

