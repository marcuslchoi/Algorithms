using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace Algorithms
{
    public class BBB
    {
        //#40 remove duplicates from unsorted linked list
        public static void RemoveDuplicates(Node head)
        {
            var set = new HashSet<int>();
            Node curr = head;
            Node prev = null;
            while (curr != null)
            {
                if (!set.Contains(curr.Data))
                {
                    set.Add(curr.Data);
                }
                else
                {
                    //go back
                    curr = prev;
                    //remove the node whose data was in the set
                    curr.Next = curr.Next.Next;
                }
                prev = curr;
                curr = curr.Next;
            }
        }

        //#15 Build order
        //topological sort, depth first search O(n) since hitting each node once
        public static List<int> BuildOrder(List<List<int>> processes)
        {
            var tempMarks = new HashSet<int>();
            var permMarks = new HashSet<int>();
            var results = new List<int>();

            for (int i = 0; i < processes.Count; i++)
            {
                Visit(i, processes, tempMarks, permMarks, results);
            }
            return results;
        }

        private static void Visit(int process, List<List<int>> processes, HashSet<int> tempMarks,
            HashSet<int> permMarks, List<int> results)
        {
            if (tempMarks.Contains(process))
            {
                Debug.WriteLine("error, loop! already in temp marks: "+process);
                throw new Exception();
            }

            if(!permMarks.Contains(process))
            {
                tempMarks.Add(process);
                foreach (var dependency in processes[process])
                {
                    Visit(dependency, processes, tempMarks, permMarks, results);
                }
                permMarks.Add(process);
                tempMarks.Remove(process);
                results.Add(process);
            }
        }

        //#43 all sets of 3 that add up to 0
        public static List<List<int>> ThreeSum(List<int> arr)
        {
            arr.Sort();
            var sets = new List<List<int>>();
            //-3 because the last 3 pointers are the inverse target, low and high
            for (int i = 0; i < arr.Count - 3; i++)
            {
                if (i > 0 && arr[i] == arr[i - 1]) continue; //skip any duplicates

                var targetSum = -arr[i];
                int low = i + 1;
                int high = arr.Count - 1;
                while (low < high)
                {
                    var currSum = arr[low] + arr[high];
                    if(currSum == targetSum)
                    {
                        var set = new List<int> { arr[i], arr[low], arr[high] };
                        sets.Add(set);
                    }
                    
                    if(currSum < targetSum)
                    {
                        int prevLowNum = arr[low];
                        //go past duplicates
                        while (arr[low] == prevLowNum && low < high)
                        {
                            prevLowNum = arr[low];
                            low++;
                        }
                    }
                    else
                    {
                        int prevHighNum = arr[high];
                        while (arr[high] == prevHighNum && low < high)
                        {
                            prevHighNum = arr[high];
                            high--;
                        }
                    }
                }
            }

            return sets;
        }

        //wrong
        private static List<List<int>> GetAllSetsOfThree(List<int> arr)
        {
            var sets = new List<List<int>>();
            if (arr.Count == 3)
            {
                var list = arr.ToList();
                sets.Add(list);
                return sets;
            }

            int first = arr[0];
            arr.RemoveAt(0);
            sets = GetAllSetsOfThree(arr);
            var moreSets = new List<List<int>>();
            foreach (var set in sets)
            {
                for (int i = 0; i < set.Count; i++)
                {
                    //copy original set
                    var newSet = new List<int> { set[0],set[1],set[2] };
                    if (newSet[i] != first)
                    {
                        newSet[i] = first;
                        moreSets.Add(newSet);
                    }
                }
            }
            sets.AddRange(moreSets);
            return sets;
        }

        public static List<List<int>> GetAllSubsets(List<int> arr)
        {
            var sets = new List<List<int>>();
            if (arr.Count == 1)
            {
                sets.Add(arr);
                sets.Add(new List<int>());
                return sets;
            }
            var first = arr[0];
            arr.RemoveAt(0);
            sets = GetAllSubsets(arr);
            var moreSets = new List<List<int>>();
            foreach (var set in sets)
            {
                var newSet = new List<int>();
                newSet.AddRange(set);
                newSet.Add(first);
                moreSets.Add(newSet);
            }
            sets.AddRange(moreSets);
            return sets;
        }
    }
}
