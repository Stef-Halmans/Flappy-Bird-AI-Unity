using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AI
{
    public class MatrixMath
    {
        static System.Random random = new System.Random();


        public static float[,] Copy(float[,] matrix)
        {
            float[,] newMatrix = new float[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }

            return newMatrix;
        }

        public static float Random(double minValue, double maxValue)
        {
            return (float)(random.NextDouble() * (maxValue - minValue) + minValue);
        }

        //Zet op alle plekken in de matrix een random getal neer tussen de minValue en maxValue als het een 2 dementionele matrix is.
        public static float[,] Randomize(int rows, int colms, double minValue, double maxValue)
        {
            float[,] newMatrix = new float[rows, colms];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colms; j++)
                {
                    newMatrix[i, j] = (float)(random.NextDouble() * (maxValue - minValue) + minValue);
                }
            }
            return newMatrix;

        }

        public static int GetIndexHighestNumber(float[] array)
        {
            float maxValue = array.Max();
            int maxIndex = array.ToList().IndexOf(maxValue);

            return maxIndex;
        }
        public static float GetIndexHighestValue(float[] array)
        {
            float maxValue = array.Max();
            return maxValue;
        }
        public static float[,] Combine(float[,] matrix1, float[,] matrix2)
        {
            float[,] newMatrix = new float[matrix1.GetLength(0), matrix1.GetLength(1)];

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    float random = Random(0, 1);
                    
                    newMatrix[i, j] = random * matrix1[i,j] + (1 - random) * matrix2[i,j];

                }
            }

            return newMatrix;
        }

        public static float[] Combine(float[] array1, float[] array2)
        {
            float[] newArray = new float[array1.Length];
            for(int i = 0; i < array1.Length; i++)
            {
                float random = Random(0, 1);

                newArray[i] = random * array1[i] + (1 - random) * array2[i];
            }

            return newArray;
        }

        public static float[,] MutateRandom(float[,] matrix, float learningRate)
        {
            float[,] newMatrix = new float[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    //if (Random(0, 1) < learningRate)
                    //{
                        newMatrix[i, j] = matrix[i,j] + Random(-learningRate, learningRate);
                    //}
                    //else
                    //{
                    //    newMatrix[i, j] = matrix[i, j];
                    //}
                }
            }

            return newMatrix;
        }

        public static float[] MutateRandom(float[] array, float learningRate)
        {
            float[] newArray = new float[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                //if (Random(0, 1) < learningRate)
                //{
                    newArray[i] = array[i] + Random(-learningRate, learningRate);
       //         }
         //       else
           //     {
               //     newArray[i] = array[i];
             //   }
            }

            return newArray;
        }


        //zet op alle plekken in de vector een random getal neer tussen de minValue en maxValue.
        public static float[] Randomize(int vectorLength, double minValue, double maxValue)
        {
            float[] newMatrix = new float[vectorLength];
            for (int i = 0; i < vectorLength; i++)
            {
                newMatrix[i] = (float)(random.NextDouble() * (maxValue - minValue) + minValue);
            }
            return newMatrix;
        }


        //Vermenigvuldigt alle getallen in de matrix met een nummer
        public static float[,] Multiply(float[,] matrix, float number)
        {
            float[,] newMatrix = new float[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[i, j] * number;
                }
            }
            return newMatrix;
        }


        //Vermenigvuldigt ieder getal van de ene matrix met het getal op de zelfde plek in de andere matrix. Ook wel het Hadamard product genoemd.
        public static float[,] Multiply(float[,] matrix1, float[,] matrix2)
        {
            float[,] newMatrix = new float[matrix1.GetLength(0), matrix2.GetLength(1)];

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix1[i, j] * matrix2[i, j];
                }
            }

            return newMatrix;
        }


        public static float[,] MatrixProduct(float[,] matrix1, float[,] matrix2)
        {
            float[,] newMatrix = new float[matrix1.GetLength(0), matrix2.GetLength(1)];

            if (matrix1.GetLength(1) != matrix2.GetLength(0)) return newMatrix;

            for (int i = 0; i < newMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < newMatrix.GetLength(1); j++)
                {
                    float sum = 0;
                    for (int k = 0; k < matrix1.GetLength(1); k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, i];
                    }
                    newMatrix[i, j] = sum;
                }
            }



            return newMatrix;
        }

        //Telt bij ieder getal van de matrix een getal op.
        public static float[,] Add(float[,] matrix, float number)
        {

            float[,] newMatrix = new float[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[i, j] + number;
                }
            }
            return newMatrix;

        }

        public static float[] Add(float[] array1, float[] array2)
        {

            float[] newArray = new float[array1.Length];
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                newArray[i] = array1[i] + array2[i];
            }
            return newArray;

        }


        //Telt ieder getal van matrix1 op bij het getal op de zelfde plek in matrix2. 
        public static float[,] Add(float[,] matrix1, float[,] matrix2)
        {
            float[,] newMatrix = new float[matrix1.GetLength(0), matrix2.GetLength(0)];

            if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1)) return newMatrix;

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return newMatrix;
        }


        //Draait de matrix om zodat de rij van de matrix de colom van de newMatrix word.
        public static float[,] Transpose(float[,] matrix)
        {
            float[,] newMatrix = new float[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[j, i] = matrix[i, j];
                }
            }

            return newMatrix;
        }

        public static float[] MatrixProduct(float[,] matrix1, float[] input_array)
        {
            float[] newArray = new float[matrix1.GetLength(0)];

            if (matrix1.GetLength(1) != input_array.Length) return newArray;

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < input_array.Length; j++)
                {
                    newArray[i] += matrix1[i, j] * input_array[j];
                }
            }

            return newArray;
        }

        
        public static float[] Sigmoid(float[] input_array)
        {

            float[] output_array = new float[input_array.Length];
            for(int i = 0; i < input_array.Length; i++)
           {
               float k = (float)Math.Exp(input_array[i]);
               output_array[i] = (float)(k / (1.0 + k));
           }
            return output_array;
        }

    }
}

