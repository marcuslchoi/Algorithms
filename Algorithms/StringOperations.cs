using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using AppKit;
//using Foundation;
namespace Algorithms
{
    public class StringOperations
    {
        public static string ReverseString(string str)
        {
            var strList = str.ToCharArray().ToList();
            var reverseList = new List<char>();
            string reverseString = "";
            for (int i = strList.Count - 1; i >= 0; i--)
            {
                reverseString += strList[i].ToString();
                //reverseList.Add(strList[i]);
            }
            return reverseString;
        }

        public static string ReverseStringRecursive(string str)
        {
            if (str == "")
            {
                return str;
            }
            else
            {
                return ReverseStringRecursive(str.Substring(1)) + str[0];
            }
        }

        //see if some letters in str1 can rearrange to create str2
        public static bool StringScramble(string str1, string str2)
        {
            var char1Dict = GetCharDictionary(str1);
            var char2Dict = GetCharDictionary(str2);

            //loop through char2 dictionary, make sure each key value pair is in char1 dictionary
            foreach (KeyValuePair<char, int> char2Entry in char2Dict)
            {
                var char2 = char2Entry.Key;
                var count2 = char2Entry.Value;

                if (!char1Dict.ContainsKey(char2) || char1Dict[char2] < count2)
                {
                    return false;
                }
            }
            return true;
        }

        //get the dictionary with character as key, its count as value
        public static Dictionary<char, int> GetCharDictionary(string str)
        {
            var charList = str.ToCharArray().ToList();
            var charDict = new Dictionary<char, int>();
            foreach (var character in charList)
            {
                var charCount = charList.Where(c => c == character).Count();
                if (!charDict.ContainsKey(character))
                {
                    charDict.Add(character, charCount);
                }
            }
            return charDict;
        }
    }
}
