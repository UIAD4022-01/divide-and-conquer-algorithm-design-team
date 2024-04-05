using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crocodile
{
    internal class Program
    {
        static int func(int level,int hot,int cold,int target,int current)
        {
            if(target == current) return level;

            if (current < target)
            {
                current = (current*level+hot)/(level + 1);
            }
            else if(current > target)
            {
                current = (current * level + cold) / (level + 1);
            }
            return func(level+1,hot,cold,target,current);
        }

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
