using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using AI;
using Unity.Collections;

namespace MultiThreading
{
    public struct PlayerThinkingJob : IJob
    {
        public NativeArray<float> inputInformation;
        public NativeArray<float> outputInformation;
        public NeuralNetwork brain;


        public void Execute()
        {
            float[] inputs = inputInformation.ToArray();
            float[] output = brain.FeedForward(inputs);
            for(int i = 0; i < output.Length; i++)
            {
                outputInformation[i] = output[i];
            }

        }
    }

}

