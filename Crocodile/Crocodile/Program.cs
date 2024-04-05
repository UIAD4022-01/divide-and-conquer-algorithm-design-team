using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crocodile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Getting quera input
            int numberOfTestCase = int.Parse(Console.ReadLine());
            List<List<int>> dataset = new List<List<int>>();

            //Putting each line of data in the dataset
            for(int i = 0; i < numberOfTestCase; i++)
            {
                String line = Console.ReadLine();
                String[] dataLine = line.Split(' ');
                dataset.Add(new List<int>());
                for (int j = 0; j < dataLine.Length; j++)
                {
                    dataset[i].Add(int.Parse(dataLine[j]));   
                }
            }
        }
    }
}
