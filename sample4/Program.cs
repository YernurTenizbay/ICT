using System;

namespace sample4
{
    public class Solution
    {
        public bool ArrayStringsAreEqual(string[] word1, string[] word2)
        {

            int w1 = 0, w2 = 0, c1 = 0, c2 = 0;

            while (w1 < word1.Length && w2 < word2.Length)
            {
                if (word1[w1][c1] != word2[w2][c2])
                    return false;

                if (c1 == word1[w1].Length - 1)
                {
                    w1++;
                    c1 = 0;
                }
                else
                    c1++;

                if (c2 == word2[w2].Length - 1)
                {
                    w2++;
                    c2 = 0;
                }
                else
                    c2++;
            }

            return w1 == word1.Length && w2 == word2.Length;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] word1 = new string[1];
            for (int i = 0; i < word1.Length; i++)
            {
                word1[i] = Console.ReadLine();
            }
            string[] word2 = new string[1];
            for (int i = 0; i < word2.Length; i++)
            {
                word2[i] = Console.ReadLine();
            }

            Solution check = new Solution();
            check.ArrayStringsAreEqual(word1, word2);

        }
    }
}
