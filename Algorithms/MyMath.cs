using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using AppKit;
//using Foundation;
namespace Algorithms
{
    public class MyMath
    {
        //recursion
        //todo dynamic programming?
        public double MyPow(double x, int n)
        {
            
            long N = n;
            Debug.WriteLine("neg n is " + -n + " neg N is "+ -N);
            if (N < 0)
            {
                N = -N;
                x = 1 / x;
            }

            
            {
                return FastPow(x, N);
            }

        }

        //n is positive
        public double FastPow(double x, long n)
        {
            if(n == 0)
            {
                return 1;
            }

            var half = FastPow(x, n / 2);
            if (n % 2 == 1)
                return half * half * x;
            else
                return half * half;
        }

        //recursion
        public int GetFibonacciNumberRecursive(int index)
        {
            int fibNumber;
            if (index < 2)
            {
                fibNumber = index;
            }
            else
            {
                fibNumber = GetFibonacciNumber(index - 1) + GetFibonacciNumber(index - 2);
            }

            return fibNumber;
        }

        public int GetFibonacciNumber(int index)
        {
            if (index < 2)
            {
                return index;
            }

            var fibNumbers = new List<int> { 0, 1 };
            Debug.Write(fibNumbers[0] + " " + fibNumbers[1] + " ");
            int fibNumber = 0;
            for (int i = 2; i <= index; i++)
            {
                fibNumber = fibNumbers[i - 2] + fibNumbers[i - 1];
                fibNumbers.Add(fibNumber);
                Debug.Write(fibNumber + " ");
            }
            Debug.WriteLine("");
            return fibNumber;
        }

        //daily code problem
        public int GetUniqueWaysToClimbSteps(int totalNumSteps)
        {
            //can increase by 1 or 2 at a time
            var possibleSteps = new List<int> { 1, 2 };

            int numListCounter = 0;
            //generate all possible arrays of numbers that add to numSteps with 1 or 2
            var numListStringDict = new Dictionary<int, string>();

            List<int> numList = new List<int>();

            for (int i = 0; i < totalNumSteps; i++) 
            {
                //loop through to make a unique list
                int currentStepCount = 0;
                for (int j = 0; j < totalNumSteps; j++)
                {
                    int currentStepSize = possibleSteps[0];
                    int stepsRemaining = totalNumSteps - currentStepCount;
                    if (stepsRemaining > 0) //there are steps remaining
                    {
                        if (stepsRemaining >= currentStepSize)
                        {
                            currentStepCount += currentStepSize;
                            numList.Add(currentStepSize);
                        }
                        else
                        {
                            //todo try with a different stepSize
                        }
                    }
                    else //no steps remaining
                    {
                        //make the numlist a string and add to dictionary
                        numListCounter++;
                        string numListString = string.Empty;
                        foreach (var num in numList)
                        {
                            numListString += num.ToString();
                        }
                        numListStringDict.Add(numListCounter, numListString);
                        break;
                    }
                }
            }

            //bool keepLooping = true;
            //while(keepLooping)
            //{
            //    for (int i = 0; i < possibleSteps.Count; i++)
            //    {
            //        numList.Add(possibleSteps[i]);
            //    }   
            //}

            return numListStringDict.Count;
        }

        //note: use 2 pointer/sorting method to remove bugs from duplicate values
        private List<List<int>> Threesum(int[] nums)
        {
            var numList = nums.ToList();
            var dict = new Dictionary<int, int>(); //value, index
            int j;
            var arrays = new List<List<int>>();
            for (int i = 0; i < numList.Count; i++)
            {
                for (j = i + 1; j < numList.Count; j++)
                {
                    //look for the complement
                    int seeking = -(numList[i] + numList[j]);
                    if (dict.ContainsKey(seeking))
                    {
                        var indexOfComplement = dict[seeking];
                        if (indexOfComplement > j)
                            arrays.Add(new List<int> { numList[i], numList[j], seeking });
                    }

                    //add the current number to dictionary
                    if (!dict.ContainsKey(numList[j]))
                    {
                        //bugs: does not account for duplicate values
                        dict.Add(numList[j], j);
                    }
                }
            }
            return arrays;
        }

        public static List<int> GetBinaryNumberList(int num, int lengthOfList)
        {
            var binNumString = Convert.ToString(num, 2);
            int strLength = binNumString.Length;
            int numberZeroesToAdd = lengthOfList - strLength;

            var binNumList = new List<int>();
            for (int i = 0; i < numberZeroesToAdd; i++)
            {
                Debug.Write(0 + " ");
                binNumList.Add(0);
            }

            foreach (var c in binNumString)
            {
                Debug.Write(c + " ");
                binNumList.Add(Convert.ToInt32(c.ToString()));
            }
            Debug.WriteLine("");
            return binNumList;
        }

        public static bool ArrayAddition(int[] arr)
        {
            var numListSorted = arr.ToList();
            numListSorted.Sort();
            var maxValue = numListSorted.Max();

            numListSorted.Remove(maxValue);

            Debug.WriteLine("looking for sum of " + maxValue);

            int numberSubsets = (int)Math.Pow(2, numListSorted.Count);

            for (int i = 0; i < numberSubsets; i++)
            {
                int sum = 0;
                var binNumList = GetBinaryNumberList(i, numListSorted.Count);
                for (var j = 0; j < binNumList.Count; j++)
                {
                    if (binNumList[j] == 1) //add this number to sum
                    {
                        sum += numListSorted[j];
                        if (sum == maxValue)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //see if any combination of numbers (excl. largest) can be added together to get largest number
        public static bool ArrayAddition1(int[] arr)
        {
            //add them all up, take away one by one in a dictionary
            //dictionary containing remaining numbers and their count
            var remainingNumsDict = new Dictionary<int, int>();

            var numListSorted = arr.ToList();
            numListSorted.Sort();
            var maxValue = numListSorted.Max();

            var currentTotal = 0;
            //get counts, add to dict
            foreach (var num in numListSorted)
            {
                //ignore the max value
                if (num == maxValue)
                {
                    continue;
                }

                //add up all other numbers
                currentTotal += num;

                var count = numListSorted.Where(n => n == num).Count();
                if (!remainingNumsDict.ContainsKey(num))
                {
                    remainingNumsDict.Add(num, count);
                }
            }

            if (currentTotal == maxValue)
            {
                return true;
            }

            while (currentTotal != maxValue)
            {
                int lookingFor = currentTotal - maxValue; //the number to subtract

                //if (numListSorted.Contains(lookingFor))
                //{
                //    return true;
                //}

                //int indexOfNextSma


                if (remainingNumsDict.ContainsKey(lookingFor))
                {
                    return true;
                }
                else
                {
                    //int nextSmallerNum;
                    //foreach (var n in numListSorted)
                    //{
                    //    if ()
                    //    }

                    foreach (KeyValuePair<int, int> entry in remainingNumsDict)
                    {
                        //get the next number smaller than the lookingfor number
                        var num = entry.Key;
                        var count = entry.Value;



                        //start subtracting
                    }
                }

            }

            return false;
        }
    }
}
