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

            if (CycleLengthThree(matrixPowerThree, n))
            {
                Console.WriteLine("YES");
            }
            else
                Console.WriteLine("NO");

        }

        public static bool CycleLengthThree(int[,] matrixPowerThree, int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (matrixPowerThree[i, i] != 0)
                {
                    return true;
                }
            }

            return false;
        }

        // Multiplying Matrix1 into Matrix2 using divide and conquer
        public static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2, int n)
        {
            int[,] result = new int[n, n];

            if (n == 1)
            {
                result[0, 0] = matrix1[0, 0] * matrix2[0, 0];
            }
            else
            {
                int[,] A11 = new int[n / 2, n / 2];
                int[,] A12 = new int[n / 2, n / 2];
                int[,] A21 = new int[n / 2, n / 2];
                int[,] A22 = new int[n / 2, n / 2];

                int[,] B11 = new int[n / 2, n / 2];
                int[,] B12 = new int[n / 2, n / 2];
                int[,] B21 = new int[n / 2, n / 2];
                int[,] B22 = new int[n / 2, n / 2];

                // Divide matrix1 into 4 submatrices
                DivideMatrix(matrix1, A11, 0, 0);
                DivideMatrix(matrix1, A12, 0, n / 2);
                DivideMatrix(matrix1, A21, n / 2, 0);
                DivideMatrix(matrix1, A22, n / 2, n / 2);

                // Divide matrix2 into 4 submatrices
                DivideMatrix(matrix2, B11, 0, 0);
                DivideMatrix(matrix2, B12, 0, n / 2);
                DivideMatrix(matrix2, B21, n / 2, 0);
                DivideMatrix(matrix2, B22, n / 2, n / 2);

                // Compute sub-results recursively
                int[,] C11 = AddMatrices(MultiplyMatrices(A11, B11, n / 2), MultiplyMatrices(A12, B21, n / 2), n / 2);
                int[,] C12 = AddMatrices(MultiplyMatrices(A11, B12, n / 2), MultiplyMatrices(A12, B22, n / 2), n / 2);
                int[,] C21 = AddMatrices(MultiplyMatrices(A21, B11, n / 2), MultiplyMatrices(A22, B21, n / 2), n / 2);
                int[,] C22 = AddMatrices(MultiplyMatrices(A21, B12, n / 2), MultiplyMatrices(A22, B22, n / 2), n / 2);

                // Combine sub-results into the result matrix
                CombineMatrices(C11, result, 0, 0);
                CombineMatrices(C12, result, 0, n / 2);
                CombineMatrices(C21, result, n / 2, 0);
                CombineMatrices(C22, result, n / 2, n / 2);
            }

            return result;
        }

        // Function to add two matrices
        public static int[,] AddMatrices(int[,] matrix1, int[,] matrix2, int n)
        {
            int[,] result = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return result;
        }

        // Function to divide a matrix into a submatrix
        public static void DivideMatrix(int[,] matrix, int[,] submatrix, int startRow, int startCol)
        {
            int n = submatrix.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    submatrix[i, j] = matrix[startRow + i, startCol + j];
                }
            }
        }

        // Function to combine a submatrix into a matrix
        public static void CombineMatrices(int[,] submatrix, int[,] matrix, int startRow, int startCol)
        {
            int n = submatrix.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[startRow + i, startCol + j] = submatrix[i, j];
                }
            }
        }

        //public static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2, int n)
        //{
        //    int[,] result = new int[n, n];

        //    for (int i = 0; i < n; i++)
        //    {
        //        for (int j = 0; j < n; j++)
        //        {
        //            for (int k = 0; k < n; k++)
        //            {
        //                result[i, j] += matrix1[i, k] * matrix2[k, j];
        //            }
        //        }
        //    }

        //    return result;
        //}

        // Reading Matrix Line by Line Separated by ' '
        public static int[,] ReadMatrix(int n)
        {
            int[,] matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(' ');

                //if (input.Length != n)
                //{
                //    Console.WriteLine($"Error: Expected {n} elements for row {i + 1}");
                //    return null;
                //}

                for (int j = 0; j < n; j++)
                    matrix[i, j] = int.Parse(input[j]);
                //{
                //    if (!int.TryParse(input[j], out matrix[i, j]))
                //    {
                //        Console.WriteLine($"Error: Invalid input '{input[j]}'");
                //        return null;
                //    }
                //}
            }

            return matrix;
        }
    }
}