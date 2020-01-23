using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using AppKit;
//using Foundation;
namespace Algorithms
{
    public class SortingOps
    {
        public static List<int> BubbleSort(List<int> nums)
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < nums.Count - 1; i++)
                {
                    if (nums[i] > nums[i + 1])
                    {
                        int temp = nums[i];
                        nums[i] = nums[i + 1];
                        nums[i + 1] = temp;
                        sorted = false;
                    }
                }
            }
            return nums;
        }

        public static List<int> SelectionSort(List<int> nums)
        {
            //put smallest at j index
            for (int j = 0; j < nums.Count; j++)
            {
                int currentSmallest = nums[j];
                int indexOfSmallest = j;
                for (int i = j; i < nums.Count; i++)
                {
                    if (nums[i] < currentSmallest)
                    {
                        currentSmallest = nums[i];
                        indexOfSmallest = i;
                    }
                }
                var temp = nums[j];
                nums[j] = currentSmallest;
                nums[indexOfSmallest] = temp;
            }
            return nums;
        }

        public static List<int> InsertionSort(List<int> nums)
        {

            for (int i = 0; i < nums.Count - 1; i++)
            {
                if (nums[i + 1] < nums[i])
                {
                    int numberToMove = nums[i + 1];
                    for (int j = i; j >= 0; j--)
                    {
                        if (nums[j] < numberToMove)
                        {
                            nums.RemoveAt(i + 1);
                            nums.Insert(j + 1, numberToMove);
                            break;
                        }
                    }
                }
            }
            return nums;
        }

        public static List<int> MergeSort(List<int> nums)
        {
            //initial condition
            if (nums.Count == 1)
            {
                return nums;
            }

            //splitting the list in half
            var count = nums.Count;
            var halfCount = count / 2;
            var leftList = nums.GetRange(0, halfCount);
            var rightList = nums.GetRange(halfCount, count - halfCount);

            if (true) //debugging
            {
                Debug.Write("[");
                foreach (var num in leftList)
                {
                    Debug.Write(num + " ");
                }
                Debug.Write("]");
                Debug.WriteLine(" ");

                Debug.Write("[");
                foreach (var num in rightList)
                {
                    Debug.Write(num + " ");
                }
                Debug.Write("]");
                Debug.WriteLine(" ");
            }

            var sorted = Merge(MergeSort(leftList), MergeSort(rightList));
            return sorted;
        }

        private static List<int> Merge(List<int> leftList, List<int> rightList)
        {
            var mergedList = new List<int>();

            int leftIndex = 0;
            int rightIndex = 0;
            while (leftIndex < leftList.Count && rightIndex < rightList.Count)
            {
                var leftNum = leftList[leftIndex];
                var rightNum = rightList[rightIndex];
                if (rightNum < leftNum)
                {
                    mergedList.Add(rightNum);
                    rightIndex++;
                }
                else
                {
                    mergedList.Add(leftNum);
                    leftIndex++;
                }
            }

            var leftRemaining = leftList.GetRange(leftIndex, leftList.Count - leftIndex);
            var rightRemaining = rightList.GetRange(rightIndex, rightList.Count - rightIndex);
            mergedList.AddRange(leftRemaining);
            mergedList.AddRange(rightRemaining);

            return mergedList;
        }
    }
}
