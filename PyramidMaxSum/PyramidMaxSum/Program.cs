using System;
using System.IO;

namespace PyramidMaxSum
{
   //Task done by: Liudvikas Paukste

    class Program
    {
        static void Main(string[] args)
        {
            int[,] numberPyramid = Load(Directory.GetCurrentDirectory() + @"\input.txt");
            int maxSum = FindMaxSum(numberPyramid);
            Console.WriteLine($"Maximum sum is: {maxSum}");
            Console.Read();
        }


        /// <summary>
        /// Applies algorithm and gives an answer to maxSum problem
        /// </summary>
        /// <param name="numPyr"></param>
        /// <returns>maxSum</returns>
        private static int FindMaxSum(int[,] numPyr)
        {
            int sizeX = numPyr.GetLength(0);
            int[] largestValues = new int[sizeX];
            

            //If first line is odd, then subsequent line should be even and so on. So let's make numbers that don't belong to corresponding lines to int.MinValue
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if ((i % 2 == 1 && numPyr[i, j] % 2 == 1) || (i % 2 == 0 && numPyr[i, j] % 2 == 0) || numPyr[i, j] == 0)
                    {
                        numPyr[i, j] = int.MinValue;
                    }
                }
            }

            //Preload the largestValues array with bottom row of numPyr
            for (int i = 0; i < sizeX; i++)
            {
                largestValues[i] = numPyr[numPyr.GetLength(0) - 1, i];
            }

            #region Explanation
            //Algorithm explanation:
            //Starting array looks like this:
            // 1 0 0 0 
            // 8 9 0 0 
            // 1 5 9 0 
            // 4 5 2 3

            //Make numbers that do not belong on a row due to being even or odd or zero to int.MinValue
            // 1 -2147483648 -2147483648 -2147483648
            // 8 -2147483648 -2147483648 -2147483648
            // 1  5           9          -2147483648  
            // 4 -2147483648  2          -2147483648

            //Preload largestValue array with bottom row 
            // [4 -2147483648  2          -2147483648]

            //Starting with penultimate row from starting array reassign largestValues array values by the following rule:
            //Take a j-th value from i-th row of starting array and add it to a j-th or j-th+1 value (whichever is larger) from largestValues array and assign it to j-th value of largestValues array
            //1st step largestValues array: [5 7 11 -2147483648*2]
            //2nd step largestValues array: [15 -2147483637 -2147483637 -2147483648*3]
            //3rd step largestValues array: [16 ... ... ...]
            //Answer: maxSum is 16


            #endregion

            //Traverse the numPyr array
            //Begin at the bottom
            for (int i = sizeX - 2; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    largestValues[j] = numPyr[i, j] + Math.Max(largestValues[j], largestValues[j + 1]);
                }
            }

            return largestValues[0];


            #region Explanation
            //Algorithm explanation:
            //Starting array looks like this:
            // 1 0 0 0 
            // 8 9 0 0 
            // 1 5 9 0 
            // 4 5 2 3

            //Make numbers that do not belong on a row due to being even or odd or zero to int.MinValue
            // 1 -2147483648 -2147483648 -2147483648
            // 8 -2147483648 -2147483648 -2147483648
            // 1  5           9          -2147483648  
            // 4 -2147483648  2          -2147483648

            //Preload largestValue array with bottom row 
            // [4 -2147483648  2          -2147483648]

            //Starting with penultimate row from starting array reassign largestValues array values by the following rule:
            //Take a j-th value from i-th row of starting array and add it to a j-th or j-th+1 value (whichever is larger) from largestValues array and assign it to j-th value of largestValues array
            //1st step largestValues array: [5 7 11 -2147483648*2]
            //2nd step largestValues array: [15 -2147483637 -2147483637 -2147483648*3]
            //3rd step largestValues array: [16 ... ... ...]
            //Answer: maxSum is 16


            #endregion
        }

        /// <summary>
        /// Loads input from string format to int[,] array
        /// </summary>
        private static int[,] Load(string filename)
        {
            string line;
            string[] linePieces;
            int lineCount = 0;
            StreamReader reader = new StreamReader(filename);

            //Counts number of lines and initializes inputPyramid array
            while (reader.ReadLine() != null)
            {               
                lineCount++;
            }

            int[,] inputPyramid = new int[lineCount, lineCount];


            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            int j = 0;
            while ((line = reader.ReadLine()) != null)
            {
                linePieces = line.Split(' ');
                for (int i = 0; i < linePieces.Length; i++)
                {
                    inputPyramid[j, i] = int.Parse(linePieces[i]);
                }
                j++;
            }

            reader.Close();

            return inputPyramid;
        }
    }
}
