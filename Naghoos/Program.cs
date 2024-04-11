using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = ReadMatrix(n);
            int[,] matrixPowerThree = MultiplyMatrices(matrix, MultiplyMatrices(matrix, matrix, n), n);
            
            

        }

        public static bool CycleLengthThree(int[,] matrixPowerThree, int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (matrixPowerThree[i,i] != 0)
                {
                    return true;
                }
            }

            return false;
        }

        // Multiplying Matrix1 into Matrix2
        public static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2, int n)
        {
            int[,] result = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            return result;
        }

        // Reading Matrix Line by Line Separated by ' '
        public static int[,] ReadMatrix(int n)
        {
            int[,] matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(' ');

                if (input.Length != n)
                {
                    Console.WriteLine($"Error: Expected {n} elements for row {i + 1}");
                    return null;
                }

                for (int j = 0; j < n; j++)
                {
                    if (!int.TryParse(input[j], out matrix[i, j]))
                    {
                        Console.WriteLine($"Error: Invalid input '{input[j]}'");
                        return null;
                    }
                }
            }

            return matrix;
        }
    }
}