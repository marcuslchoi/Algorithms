using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public interface ICloneable<T>
    {
        T Clone();
    }

    public class Book : ICloneable<Book>
    {
        public Book Clone()
        {
            return new Book { /* set properties */ };
        }
    }

    public class LeetcodeApp
    {
        //this doesn't work
        public static int WaysToNavigateMaze(int[,] maze, int r, int c)
        {
            if (r == 0 || c == 0)
            {
                return 0;
            }

            int lastIndexR = r - 1;
            int lastIndexC = c - 1;

            int ways = WaysToNavigateMaze(maze, lastIndexR - 1, lastIndexC) +
                WaysToNavigateMaze(maze, lastIndexR, lastIndexC - 1);
            return ways;
        }

        public static Node FindKthToLastNode(DoublyLinkedList list, int k)
        {
            int N = list.Length;
            if (k > N) { return null; }
            else if (k == N) { return list.Head; }

            //N - k = nth node
            //find node k+1 from last, get its next
            Node kthPlus1Node = FindKthToLastNode(list, k + 1);
            Node kthNode = kthPlus1Node.Next;

            return kthNode;
        }

        //56 merge intervals
        public void SortTheDamnListsForMerge(List<List<int>> lists)
        {
            //index, list
            //var dict = new Dictionary<int, List<int>>
        }

        public int[][] Merge(int[][] intervals)
        {
            var intervalsL = new List<List<int>>();
            foreach (var interval in intervals)
            {
                intervalsL.Add(interval.ToList());
            }

            intervalsL = intervalsL.OrderBy(i => i[0]).ToList();
            //intervalsL.OrderBy(i => i[1]).ToList();

            bool canMerge = true;

            int highestIndex = 0;
            while (canMerge)
            {
                canMerge = false;
                for (int i = highestIndex; i < intervalsL.Count - 1; i++)
                {
                    var firstInterval = intervalsL[i];
                    var secondInterval = intervalsL[i + 1];
                    if (firstInterval[1] >= secondInterval[0])
                    {
                        //can merge
                        canMerge = true;
                        var secondNum = Math.Max(firstInterval[1], secondInterval[1]);
                        var mergedList = new List<int> { firstInterval[0], secondNum };
                        intervalsL.RemoveRange(i, 2); //remove the old intervals
                        intervalsL.Insert(i, mergedList); //insert the merged interval
                        i = highestIndex;
                        break;
                    }
                }
            }
            var merged = new int[intervalsL.Count][];
            for (int i = 0; i < intervalsL.Count; i++)
            {
                merged[i] = intervalsL[i].ToArray();
            }
            return merged;
        }

        public List<string> GetPerms(string str)
        {
            var perms = new List<string>();
            if (str.Length == 0)
            {
                perms.Add("");
                return perms;
            }
            var first = str[0];
            var remainder = str.Substring(1);

            var permsRem = GetPerms(remainder);
            foreach (var permRem in permsRem)
            {
                for (int i = 0; i <= permRem.Length; i++)
                {
                    var perm = InsertCharAtIndex(i, first, permRem);
                    perms.Add(perm);
                }
            }
            return perms;
        }

        private string InsertCharAtIndex(int i, char c, string str)
        {
            var start = str.Substring(0, i);
            var end = str.Substring(i);
            var newStr = start + c + end;
            return newStr;
        }

        //46 get all permutations of given array of numbers
        public IList<IList<int>> Permute(int[] nums)
        {
            string numsStr = string.Empty;
            foreach (int num in nums)
            {
                numsStr += num.ToString();
            }

            var permStrings = this.GetPerms(numsStr);
            var permutations = new List<IList<int>>();

            foreach (var permStr in permStrings)
            {
                var perm = new List<int>();
                foreach (var c in permStr)
                {
                    var temp = c.ToString();
                    perm.Add(int.Parse(temp));
                }
                permutations.Add(perm);
            }
            
            return permutations;
        }

        public static void NextPermutation(int[] nums)
        {
            //going from right, find the first num that is smaller than previous
            int indexOfFirstSmaller = -1;
            for (int i = nums.Length - 1; i > 0; i--)
            {
                if (nums[i-1] < nums[i])
                {
                    indexOfFirstSmaller = i - 1;
                    break;
                }
            }

            if (indexOfFirstSmaller == -1)
            {
                var numList = nums.ToList();
                numList.Sort();
                //numList.Reverse();
                nums = numList.ToArray();
            }
            else
            {
                int firstSmaller = nums[indexOfFirstSmaller];
                //find the next smallest num and swap it with the first smaller
                int nextSmall = -1;
                int indexOfNextSmall = -1;
                for(int i = indexOfFirstSmaller+1; i< nums.Length;i++)
                {
                    var num = nums[i];
                    if (num > firstSmaller)
                    {
                        if (nextSmall == -1 || num < nextSmall)
                        {
                            nextSmall = num;
                            indexOfNextSmall = i;
                        }
                    }
                }

                //swap
                nums[indexOfFirstSmaller] = nextSmall;
                nums[indexOfNextSmall] = firstSmaller;

                //sort nums after index of first smaller
                //bubble sort
                bool sorted = false;
                while (!sorted)
                {
                    sorted = true;
                    for (int i = indexOfFirstSmaller+1; i < nums.Length - 1; i++)
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
            }
        }

        //1. use 2 pointer method
        //return indices of numbers that add to target
        public int[] TwoSum(int[] nums, int target)
        {
            //sort the nums list
            var numlist = nums.ToList();
            numlist.Sort();
            int low = 0;
            int high = numlist.Count - 1;
            while (low < high)
            {
                var lowNum = numlist[low];
                var highNum = numlist[high];
                var sum = lowNum + highNum;
                if (sum == target)
                {
                    var lowNumIndex = -1;
                    var highNumIndex = -1;

                    for (int i = 0; i < nums.Length; i++)
                    {
                        if (nums[i] == lowNum && lowNumIndex == -1)
                        {
                            lowNumIndex = i;
                        }
                        else if (nums[i] == highNum && highNumIndex == -1)
                        {
                            highNumIndex = i;
                        }
                    }
                    return new int[] { lowNumIndex, highNumIndex };
                }
                else if (sum < target)
                {
                    low++;
                }
                else
                {
                    high--;
                }
            }
            return null;
        }

        //15
        //use 2 pointers method
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            
            var numList = nums.ToList();
            numList.Sort();

            var ans = new List<IList<int>>();

            for (int i = 0; i < numList.Count-2; i++)
            {
                //check if previous number was same as current number
                if (i > 0 && numList[i] == numList[i - 1]) continue; // skip

                var x = numList[i];
                //find y and z such that their sum is target
                var target = -x;
                var low = i + 1;
                var high = numList.Count - 1;

                while (low < high)
                {
                    var y = numList[low];
                    var z = numList[high];
                    if (y + z == target)
                    {
                        ans.Add(new List<int> { x, numList[low], numList[high] });

                        //make sure to skip any duplicate numbers
                        while (low < high && numList[low] == numList[low + 1]) low++;
                        while (low < high && numList[high] == numList[high - 1]) high--;

                        //increment to next numbers
                        low++;
                        high--;
                    }
                    else if (y + z < target)
                    {
                        low++;
                    }
                    else
                    {
                        high--;
                    }
                }
            }
            return ans;
        }

        //15
        //this one finds all with duplicates (broken)
        public IList<IList<int>> ThreeSumDict(int[] nums)
        {
            //x+y+z = 0
            //target is z
            //z = -(x+y)
            //loop through the x's

            var dict = new Dictionary<int, int>();
            foreach (var num in nums)
            {
                if (dict.ContainsKey(num))
                {
                    dict[num]++;
                }
                else
                {
                    dict.Add(num, 1);
                }
            }

            var lists = new List<IList<int>>();
            if (dict.ContainsKey(0) && dict[0] >= 3)
            {
                lists.Add(new List<int> { 0, 0, 0 });
            }

            var doneDict = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> entry in dict)
            {
                foreach (KeyValuePair<int, int> entry1 in dict)
                {
                    if (doneDict.ContainsKey(entry1.Key)) { continue; }
                    var x = entry.Key;
                    var y = entry1.Key;
                    if (x==y && entry.Value < 2)
                    {
                        continue;
                    }
                    var target = -(x+y);
                    if (dict.ContainsKey(target))
                    {
                        lists.Add(new List<int> {x, y, target });
                        if (!doneDict.ContainsKey(entry1.Key))
                        {
                            doneDict.Add(entry1.Key, entry1.Value);
                        }
                    }
                }
                doneDict.Add(entry.Key, entry.Value);
            }

            return lists;
        }

        //20. valid parentheses
        public bool IsValid(string s)
        {
            if (s == String.Empty) { return true; }
            var mustCloseStack = new Stack<char>();

            var opensDict = new Dictionary<char, int>();
            opensDict.Add('{', 0); opensDict.Add('[', 0); opensDict.Add('(', 0);

            var closedDict = new Dictionary<char, char>();
            closedDict.Add('}', '{'); closedDict.Add(']', '['); closedDict.Add(')', '(');

            foreach (var c in s)
            {
                if (opensDict.ContainsKey(c)) //is open parenthesis
                {
                    mustCloseStack.Push(c); 
                }
                else //is close parenthesis
                {
                    if (mustCloseStack.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        //stack.pop must equal the valid open parenthesis
                        var top = mustCloseStack.Pop();
                        if (closedDict[c] != top)
                        {
                            return false;
                        }
                    }
                }
            }
            return mustCloseStack.Count == 0;
        }

        //trap rain water
        public int Trap(int[] height)
        {
            int totalWater = 0;
            var heightList = height.ToList();

            var lMaxDict = GetMaxHeightDictL(heightList);
            var rMaxDict = GetMaxHeightDictR(heightList);

            for (int i = 0; i < heightList.Count;i++)
            {
                var currHeight = heightList[i];
                var lMax = lMaxDict[i];//Max(heightList.GetRange(0, i));
                var rMax = rMaxDict[i];//Max(heightList.GetRange(i + 1, heightList.Count - (i + 1)));
                var min = Math.Min(lMax, rMax);
                if (min > currHeight)
                {
                    totalWater += (min - currHeight);
                }
            }
            return totalWater;
        }

        public Dictionary<int, int> GetMaxHeightDictL(List<int> heightList)
        {
            //index, height of highest
            var dict = new Dictionary<int, int>();

            int currentMax = 0;
            for(int i = 0; i < heightList.Count;i++)
            {
                dict.Add(i, currentMax);

                var currHeight = heightList[i];
                if(currHeight > currentMax)
                {
                    currentMax = currHeight;
                }
            }
            return dict;
        }

        public Dictionary<int, int> GetMaxHeightDictR(List<int> heightList)
        {
            //index, height of highest
            var dict = new Dictionary<int, int>();

            int currentMax = 0;
            for (int i = heightList.Count-1; i >= 0; i--)
            {
                dict.Add(i, currentMax);

                var currHeight = heightList[i];
                if (currHeight > currentMax)
                {
                    currentMax = currHeight;
                }
            }
            return dict;
        }

        //get the max value
        public int Max(List<int> height)
        {
            var maxH = 0;
            foreach (var h in height)
            {
                if (h > maxH)
                {
                    maxH = h;
                }
            }
            return maxH;
        }

        //11 container with the most water
        //2 pointers
        public int MaxArea(int[] height)
        {
            int i = 0;
            int j = height.Length - 1;
            var currentMaxArea = 0;
            while (i < j)
            {
                var leftHeight = height[i];
                var rightHeight = height[j];
                var length = j - i;
                var area = 0;
                if (leftHeight <= rightHeight)
                {
                    area = leftHeight * length;
                    i++;
                }
                else
                {
                    area = rightHeight * length;
                    j--;
                }
                if (area > currentMaxArea)
                {
                    currentMaxArea = area;
                }
            }
            return currentMaxArea;
        }

        public int MaxAreaBruteForce(int[] height)
        {
            var maxIndex = height.Length - 1;
            int currentMaxArea = 0;
            for (int i = 0; i <= maxIndex; i++)
            {
                var currentHeight = height[i];
                //go from last index to i and find the first instance of
                //a smaller number than at i
                for (int j = maxIndex; j > i; j--)
                {
                    //check if the max that area to right could be is smaller than current max area
                    if (currentHeight * (j-i) <= currentMaxArea)
                    {
                        break;
                    }
                    else if (height[j] >= currentHeight)
                    {
                        var areaToRight = (j - i) * currentHeight;
                        if (areaToRight > currentMaxArea)
                        {
                            currentMaxArea = areaToRight;
                        }
                        break;
                    }
                }

                for (int k = 0; k < i; k++)
                {
                    //check if the max that area to left could be is smaller than current max area
                    if (currentHeight * (i - k) <= currentMaxArea)
                    {
                        break;
                    }
                    else if (height[k] >= currentHeight)
                    {
                        var areaToLeft = (i - k) * currentHeight;
                        if (areaToLeft > currentMaxArea)
                        {
                            currentMaxArea = areaToLeft;
                        }
                        break;
                    }
                }
            }
            return currentMaxArea;
        }

        static Dictionary<int, string> kthGrammarRowDict = new Dictionary<int, string>();
        public static int KthGrammar(int N, int K)
        {
            bool kIsOdd = K % 2 == 1;
            int switchNumCount = kIsOdd? 0:1;
            if (kIsOdd) { K++; }
            while (K / 2 > 1)
            {
                K = K / 2;
                kIsOdd = K % 2 == 1;
                if (kIsOdd) { K++; }
                else
                {
                    switchNumCount++;
                }
            }

            if (switchNumCount % 2 == 1)
                return 1;
            else
                return 0;

            //var nthString = GenerateKthGrammarString(N);
            //var kthChar = nthString[K - 1];
            //return int.Parse(kthChar.ToString());
        }

        private static string GenerateKthGrammarString(int N)
        {
            if (kthGrammarRowDict.ContainsKey(N))
            {
                return kthGrammarRowDict[N];
            }

            //N is 1-indexed
            if (N == 1)
            {
                return "0";
            }

            var prevString = GenerateKthGrammarString(N - 1);
            var currString = string.Empty;
            foreach (var num in prevString)
            {
                if (num == '0')
                {
                    currString += "01";
                }
                else if (num == '1')
                {
                    currString += "10";
                }
            }

            kthGrammarRowDict.Add(N, currString);

            return currString;
        }

        public static Node AddTwoNumbersR(Node n1, Node n2)
        {
            if (n1 == null) { return n2; }
            else if (n2 == null) { return n1; }

            var sum = n1.Data + n2.Data;
            if (sum >= 10)
            {
                sum -= 10;
                if (n1.Next != null)
                {
                    n1.Next.Data++;
                }
                else if (n2.Next != null)
                {
                    n2.Next.Data++;
                }
                else
                {
                    n1.Next = new Node(1);
                }
            }
            var nSum = new Node(sum);
            nSum.Next = AddTwoNumbersR(n1.Next, n2.Next);
            return nSum;
        }

        //sliding window approach
        public static int LengthOfLongestSubstring(String s)
        {
            var set = new HashSet<char>();
            int n = s.Length;
            int ans = 0, i = 0, j = 0;
            while (i < n && j < n)
            {
                // try to extend the range [i, j]
                if (!set.Contains(s[j]))
                {
                    set.Add(s[j++]);
                    ans = Math.Max(ans, j - i);
                }
                else
                {
                    set.Remove(s[i++]);
                }
            }
            return ans;
        }

        //sliding window optimized
        public int LengthOfLongestSubstring2(String s)
        {
            int n = s.Length, ans = 0;
            var map = new Dictionary<char, int>(); // current index of character
                                                   // try to extend the range [i, j]
            for (int j = 0, i = 0; j < n; j++)
            {
                if (map.ContainsKey(s[j]))
                {
                    int lastIndexOfChar = map[s[j]];
                    i = Math.Max(lastIndexOfChar, i);
                    map.Remove(s[j]);
                }

                ans = Math.Max(ans, j - i + 1);
                map.Add(s[j], j + 1);
            }
        
            return ans;
        }
        


        //#3
        //find length of longest substring with non-repeating characters
        public static int LengthOfLongestSubstringNotWorking(string str)
        {
            var dict = new Dictionary<char, int>();
            var counts = new List<int>();
            counts.Add(0);
            for(int i = 0; i < str.Length; i++) 
            {
                var c = str[i];

                if (dict.ContainsKey(c))
                {
                    var distToLastOccurence = i - dict[c];
                    //update the latest index of the character
                    dict[c] = i;
                    counts.Add(distToLastOccurence);
                }
                else
                {
                    //add 1 to the last index of counts array
                    var lastIndex = counts.Count - 1;
                    counts[lastIndex]++;
                    dict.Add(c, i);
                }
            }
            counts.Sort();
            var lastIndexSorted = counts.Count - 1;
            return counts[lastIndexSorted];
        }

        //#2
        //take 2 numbers as (singly) linked list, return linked list of them added
        public static Node AddTwoNumbers(Node n1, Node n2)
        {
            int num1 = GetNumWithLinkedList(n1);
            int num2 = GetNumWithLinkedList(n2);
            int sum = num1 + num2;

            double divisor = 10;
            int numDigits = 1;
            while ((Math.Floor(sum / divisor)) != 0)
            {
                numDigits++;
                divisor *= 10;
            }
            if (numDigits == 1)
            {
                return new Node(sum);
            }

            Node head = new Node(GetDigit(sum, 1));
            Node currentNode = head;
            var position = 2;
            for (int i = 1; i < numDigits; i++)
            {
                currentNode.Next = new Node();
                currentNode = currentNode.Next; //iterate
                currentNode.Data = GetDigit(sum, position);
                position++;
            }
            return head;
        }

        //position is the digit position, ie 1st digit, 2 digit, etc starting from 1s
        private static int GetDigit(int num, int position)
        {
            double divided = num / (Math.Pow(10, position));
            int trunc = (int)Math.Truncate(divided);
            double dec = divided - trunc;
            //round it to position digits
            dec = Math.Round(dec, position);
            int digit = (int)Math.Truncate(dec * 10);
            return digit;
        }

        private static int GetNumWithLinkedList(Node n)
        {
            int num = 0;
            Node currentNode = n;
            var multiplier = 1;
            while (currentNode != null)
            {
                num += currentNode.Data * multiplier;
                multiplier *= 10;
                currentNode = currentNode.Next;
            }
            return num;
        }
    }
}
