

// Mohammadamin Nasiri            ID: "4013613080"
// Tohid Noori                    ID: "4013613082"
// Setayesh Varaei Yeganeh        ID: "4013623022"

using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = ReadMatrix(n);
            int[,] matrixPowerThree = Multiply(matrix, Multiply(matrix, matrix));
            //PrintMatrix(matrixPowerThree, n);
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

        // Multiplying Matrix1 into Matrix2 using divide and conquer - Strassen
        public static int[,] Multiply(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);
            int[,] R = new int[n, n];

            /** base case **/
            if (n == 1)
            {
                R[0, 0] = A[0, 0] * B[0, 0];
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


                /** Dividing matrix A into 4 halves **/
                Split(A, A11, 0, 0);
                Split(A, A12, 0, n / 2);
                Split(A, A21, n / 2, 0);
                Split(A, A22, n / 2, n / 2);

                /** Dividing matrix B into 4 halves **/
                Split(B, B11, 0, 0);
                Split(B, B12, 0, n / 2);
                Split(B, B21, n / 2, 0);
                Split(B, B22, n / 2, n / 2);


                /** 
                  M1 = (A11 + A22)(B11 + B22)
                  M2 = (A21 + A22) B11
                  M3 = A11 (B12 - B22)
                  M4 = A22 (B21 - B11)
                  M5 = (A11 + A12) B22
                  M6 = (A21 - A11) (B11 + B12)
                  M7 = (A12 - A22) (B21 + B22)
                **/
                int[,] M1 = Multiply(Add(A11, A22), Add(B11, B22));
                int[,] M2 = Multiply(Add(A21, A22), B11);
                int[,] M3 = Multiply(A11, Sub(B12, B22));
                int[,] M4 = Multiply(A22, Sub(B21, B11));
                int[,] M5 = Multiply(Add(A11, A12), B22);
                int[,] M6 = Multiply(Sub(A21, A11), Add(B11, B12));
                int[,] M7 = Multiply(Sub(A12, A22), Add(B21, B22));

                /**
                  C11 = M1 + M4 - M5 + M7
                  C12 = M3 + M5
                  C21 = M2 + M4
                  C22 = M1 - M2 + M3 + M6
                **/
                int[,] C11 = Add(Sub(Add(M1, M4), M5), M7);
                int[,] C12 = Add(M3, M5);
                int[,] C21 = Add(M2, M4);
                int[,] C22 = Add(Sub(Add(M1, M3), M2), M6);

                /** join 4 halves into one result matrix **/
                Join(C11, R, 0, 0);
                Join(C12, R, 0, n / 2);
                Join(C21, R, n / 2, 0);
                Join(C22, R, n / 2, n / 2);
            }

            /** return result **/
            return R;
        }

        /** Function to sub two matrices **/
        public static int[,] Sub(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);
            int[,] C = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = A[i, j] - B[i, j];
                }
            }

            return C;
        }

        /** Function to add two matrices **/
        public static int[,] Add(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);
            int[,] C = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    C[i, j] = A[i, j] + B[i, j];
            return C;
        }
        /** Function to split parent matrix into child matrices **/
        public static void Split(int[,] P, int[,] C, int iB, int jB)
        {
            for (int i1 = 0, i2 = iB; i1 < C.GetLength(0); i1++, i2++)
                for (int j1 = 0, j2 = jB; j1 < C.GetLength(0); j1++, j2++)
                    C[i1, j1] = P[i2, j2];
        }
        /** Function to join child matrices intp parent matrix **/
        public static void Join(int[,] C, int[,] P, int iB, int jB)
        {
            for (int i1 = 0, i2 = iB; i1 < C.GetLength(0); i1++, i2++)
                for (int j1 = 0, j2 = jB; j1 < C.GetLength(0); j1++, j2++)
                    P[i2, j2] = C[i1, j1];
        }

        public static void PrintMatrix(int[,] matrix, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Reading Matrix Line by Line Separated by ' '
        public static int[,] ReadMatrix(int n)
        {
            int[,] matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(' ');

                for (int j = 0; j < n; j++)
                    matrix[i, j] = int.Parse(input[j]);
            }

            return matrix;
        }
    }
}