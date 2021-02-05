using System;

namespace sample3
{
    public class Solution
    {
        public int NumberOfSteps(int num)
        {
            int sum = 0;
            while (num != 0)
            {
                if (num % 2 == 0)
                {
                    num /= 2;
                    sum++;
                }
                else
                {
                    num -= 1;
                    sum++;
                }
            }
            return sum;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int num = Convert.ToInt32(Console.ReadLine());
            Solution steps = new Solution();
            Console.WriteLine(steps.NumberOfSteps(num));
        }
    }
}
