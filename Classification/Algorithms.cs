using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{
    public class Words
    {
        public string Word { get; set; }
        //public double Weight { get; set; }
        //public int? Sign { get; set; }
        //public bool IsImportant { get; set; }
        public int Count { get; set; }
        public int index { get; set; }
        public int CountPos { get; set; }
        public int CountNeg { get; set; }
    }
    public class ExcelDataSt
    {
        public string text = "";
        public int lable = -1;
    }

    class Algorithms
    {
        public string results = "";
        private int elementLevel = -1;
        private int numberOfElements;
        private int[] permutationValue = new int[0];

        private char[] inputSet;
        public char[] InputSet
        {
            get { return inputSet; }
            set { inputSet = value; }
        }

        private int permutationCount = 0;
        public int PermutationCount
        {
            get { return permutationCount; }
            set { permutationCount = value; }
        }

        public char[] MakeCharArray(string InputString)
        {
            char[] charString = InputString.ToCharArray();
            Array.Resize(ref permutationValue, charString.Length);
            numberOfElements = charString.Length;
            return charString;
        }

        /// <summary>
        /// Recursive Algorithm Source:
        /// Counting And Listing All Permutations from Interactive Mathematics Miscellany and Puzzles
        /// </summary>
        /// <param name="k"></param>
        public void Recursion(int k)
        {
            elementLevel++;
            permutationValue.SetValue(elementLevel, k);

            if (elementLevel == numberOfElements)
            {
                //OutputPermutation(permutationValue);
                foreach (int i in permutationValue)
                {
                    results += inputSet.GetValue(i - 1);
                }
                PermutationCount++;
                results += "\n";
            }
            else
            {
                for (int i = 0; i < numberOfElements; i++)
                {
                    if (permutationValue[i] == 0)
                    {
                        Recursion(i);
                    }
                }
            }
            elementLevel--;
            permutationValue.SetValue(0, k);
        }

        /// <summary>
        /// Code Source (AddItem()):
        /// Counting And Listing All Permutations from Interactive Mathematics Miscellany and Puzzles
        /// </summary>
        /// <param name="value"></param>
        private void OutputPermutation(int[] value)
        {
            foreach (int i in value)
            {
                Console.Write(inputSet.GetValue(i - 1));
            }
            Console.WriteLine();
            PermutationCount++;
        }
    }
}
