using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VenturasContest
{
    class Program
    {

        static void Main(string[] args) 
        {
            
            var resultString = GetShortestUniqueSubstring("aaffhkksemckelloe", "fhea");
            Console.WriteLine(resultString);

            var result = minWin("ahffaksfajeeubsne", "jefaa");
            Console.WriteLine(result);
            result = minWin2("ahffaksfajeeubsne", "jefaa");
            Console.WriteLine(result);
//            var input = Convert.ToInt32(Console.ReadLine());


//            var count = 0;
//            for (int i = 1; i < 1001;i++)
//            {
//                if (IsPrime(i))
//                {
//                    count++;
//                }
//
////                Console.WriteLine(i+" is prime"+IsPrime(i));
//            }
//            Console.WriteLine(count);
//            Console.WriteLine(DuplicateList(new int[] {1, 2, 3}));
//            Console.Out.WriteLine(FizzBizz(8));            

        }

        public static string FizzBizz(in    t number)
        {
            string output = "";
            for (int i = 1; i <= number; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
//                    Console.WriteLine("Fizz"); 
                    output += "FizzBuzz";
                }
                else if (i % 3 == 0)
                {
//                    Console.WriteLine("Buzz");
                    output += "Fizz";
                }
                else if (i % 5 == 0)
                {
                    output += "Buzz";
//                    Console.WriteLine("FizzBuzz");
                }
                
                else
                {
//                    Console.WriteLine(i);
                    output += i.ToString();
                }

                if (i != number)
                {
                    output += " ";
                }
            }
            return output;
        }
        public static int DuplicateList(int [] list)
        {
            Dictionary<int, int> elementCount= new Dictionary<int, int>();
            int count = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (!elementCount.ContainsKey(list[i]))
                {
                    elementCount.Add(list[i], 0);
                }
                else
                {
//                    elementCount[list[i]]++;
                    count++;
                }
            }
            return count;
        }
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public static string GetShortestUniqueSubstring(string search, string source)
        {
            if (search == null || search.Length == 0)
            {
                return "";
            }

            // assume that search string is not empty
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }

            // put unique chars in search string to the dictionary
            var map = new Dictionary<char, int>();
            foreach (var item in search)
            {

                if (map.ContainsKey(item))
                {
                    map.Add(item, 1);
                }
            }

            var needChars = search.Length; // 'xyz' - 3, var match = needChars == 0 
            // iterate the string and find match, and also keep track of minimum 
            var left = 0;
            var length = source.Length;

            var smallestLength = length + 1;
            var smallestSubstring = "";

            for (int index = 0; index < length; index++)
            {
                var visit = source[index];

                var inMap = map.TryGetValue(visit, out var count);
                var needOne = inMap && count > 0;
                if (inMap)
                {
                    map[visit]--;
                }

                if (needOne)
                {
                    needChars--;
                }

                var findMatch = needChars == 0;
                if (!findMatch)
                {
                    continue;
                }

                // move left point forward - while loop                
                while (left <= index && (!map.ContainsKey(source[left]) || map[source[left]] < 0))
                {
                    var removeChar = source[left];

                    // update the variable needChars                     
                    if (map.ContainsKey(source[left]))
                    {
                        map[removeChar]++;
                    }

                    left++;
                }

                var currentLength = index - left + 1;
                var findSmallerOne = currentLength < smallestLength;
                if (findSmallerOne)
                {
                    smallestLength = currentLength;
                    smallestSubstring = source.Substring(left, currentLength);

                    needChars++;
                    map[source[left]]++;
                    left = left + 1;
                }
            }

            // edge case
            return smallestLength == length + 1
                ? string.Empty
                : smallestSubstring;
        }

        public static String minWin(String s, String t)
        {
            Dictionary<char, int> targetMap = new Dictionary<char, int>();
            char[] targetCharacters = t.ToCharArray();

            for (int i = 0; i < targetCharacters.Length; i++)
            {
                char c = targetCharacters[i];
                if (targetMap.ContainsKey(c))
                {
                    try
                    {
                        targetMap.Add(c, targetMap[c] + 1);
                    }
                    catch
                    {
                        
                    }
                }
                else
                {
                    targetMap.Add(c, 1);
                }
            }

            char[] sourceCharacters = s.ToCharArray();
            Dictionary<char, int> sourceMap = new Dictionary<char, int>();
            int count = 0;
            int index = 0;
            for (int i = 0; i < sourceCharacters.Length; i++)
            {
                char c = sourceCharacters[i];
                if (targetMap.ContainsKey(c))
                {
                    if (sourceMap.ContainsKey(c))
                    {
                        // str = AABC, target = ABC ==> That way we don't increment count 
                        // when targetMap.containsKey('A') is fulfilled twice for the 2 'A's.
                        if (sourceMap[c] < targetMap[c])
                        {
                            count++;
                        }

                        try
                        {
                            sourceMap.Add(c, sourceMap[c] + 1);
                        }
                        catch
                        {
                            
                        }
                    }
                    else
                    {
                        sourceMap.Add(c, 1);
                        count++;
                    }
                }

                if (count == t.Length)
                {
                    index = i;
                    break;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(s.Substring(0, index + 1));
            int minLen = sb.Length;
            String window = sb.ToString();
            for (int i = index + 1; i < sourceCharacters.Length; i++)
            {
                char c = sourceCharacters[i];
                sb.Append(c);
                if (sourceMap.ContainsKey(c))
                {
                    try
                    {
                        sourceMap.Add(c, sourceMap[c] + 1);
                    }
                    catch 
                    {
                        
                    }
                }
                else
                {
                    sourceMap.Add(c, 1);
                }

                if (c == sb[0])
                {
                    // remove extra characters
                    int tmp = 0;
                    char tmpChar = sb[tmp];
                    while (!targetMap.ContainsKey(tmpChar) || (targetMap.ContainsKey(tmpChar) && sourceMap[tmpChar] > targetMap[tmpChar]))
                    {
                        if (targetMap.ContainsKey(tmpChar) && sourceMap[tmpChar] > targetMap[tmpChar])
                        {
                            try
                            {
                                sourceMap.Add(tmpChar, sourceMap[tmpChar] - 1);
                            }
                            catch
                            {
                                
                            }
                        }
                        tmp++;
                        tmpChar = sb[tmp];
                    }

                    if (sb.ToString(tmp, sb.Length).Length <= minLen)
                    {
                        window = sb.ToString(tmp, sb.Length);
                        //                        sb.replace(0, sb.Length, window);
                        sb.Remove(0, sb.Length);
                        sb.Insert(0, window);
                        minLen = window.Length;
                    }
                }
            }


            return window;
        }
        public static String minWin2(String s, String t)
        {
            if (s.Length == 0 || t.Length == 0)
            {
                return "";
            }

            // Dictionary which keeps a count of all the unique characters in t.
            Dictionary<char, int> dictT = new Dictionary<char, int>();
            for (int i = 0; i < t.Length; i++)
            {                
                int count = 0;
                dictT.TryGetValue(t[i], out count);
                try
                {
                    dictT.Add(t[i], count + 1);
                }
                catch 
                {
                    
                }
            }

            // Number of unique characters in t, which need to be present in the desired window.
            int required = dictT.Count;

            // Left and Right pointer
            int l = 0, r = 0;

            // formed is used to keep track of how many unique characters in t
            // are present in the current window in its desired frequency.
            // e.g. if t is "AABC" then the window must have two A's, one B and one C.
            // Thus formed would be = 3 when all these conditions are met.
            int formed = 0;

            // Dictionary which keeps a count of all the unique characters in the current window.
            Dictionary<char, int> windowCounts = new Dictionary<char, int>();

            // ans list of the form (window length, left, right)
            int[] ans = { -1, 0, 0 };

            while (r < s.Length)
            {
                // Add one character from the right to the window
                char c = s[r];
                int count = 0;
                windowCounts.TryGetValue(c, out count);
                try
                {
                    windowCounts.Add(c, count + 1);
                }
                catch 
                {
                    
                }

                // If the frequency of the current character added equals to the
                // desired count in t then increment the formed count by 1.
                if (dictT.ContainsKey(c) && windowCounts[c] == dictT[c])
                {
                    formed++;
                }

                // Try and contract the window till the point where it ceases to be 'desirable'.
                while (l <= r && formed == required)
                {
                    c = s[l];
                    // Save the smallest window until now.
                    if (ans[0] == -1 || r - l + 1 < ans[0])
                    {
                        ans[0] = r - l + 1;
                        ans[1] = l;
                        ans[2] = r;
                    }

                    // The character at the position pointed by the
                    // `Left` pointer is no longer a part of the window.
                    try
                    {
                        windowCounts.Add(c, windowCounts[c] - 1);
                    }
                    catch
                    {
                        
                    }
                    if (dictT.ContainsKey(c) && windowCounts[c] < dictT[c])
                    {
                        formed--;
                    }

                    // Move the left pointer ahead, this would help to look for a new window.
                    l++;
                }

                // Keep expanding the window once we are done contracting.
                r++;
            }

            return ans[0] == -1 ? "" : s.Substring(ans[1], ans[2] + 1);
        }

//        public static string MinSub3(string s, string t)
//        {
//            Dictionary<Char, int> tChars = new Dictionary<Char, int>();
//            for (char c : t.toCharArray())
//            {
//                tChars.compute(c, (key, value)->value == null ? 1 : ++value);
//            }
//
//            Map<Character, Integer> sChars = new HashMap<>();
//            String windowSubstr = "";
//            String minSubstr = "";
//            int i = 0;
//            int j = 0;
//            int matched = 0;
//
//            while (i < s.length())
//            {
//                Character c;
//                while (j < s.length() && matched < tChars.size())
//                {
//                    c = s.charAt(j++);
//                    sChars.compute(c, (key, value)->value == null ? 1 : ++value);
//                    windowSubstr += c;
//
//                    if (tChars.containsKey(c) && tChars.get(c).equals(sChars.get(c))) matched++;
//                }
//
//                if (matched == tChars.size() && (minSubstr.equals("") || windowSubstr.length() < minSubstr.length()))
//                {
//                    minSubstr = windowSubstr;
//                }
//
//                c = windowSubstr.charAt(0);
//                sChars.compute(c, (key, value)-> --value);
//                if (tChars.containsKey(c) && tChars.get(c) > sChars.get(c)) matched--;
//                windowSubstr = windowSubstr.substring(1);
//                i++;
//            }
//
//            return minSubstr;
//
//        }
    }
}
