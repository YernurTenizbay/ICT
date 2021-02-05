using System;

namespace sample1
{
    public class Solution
    {
        public int[] RunningSum(int[] nums)
        {
            int sum = 0;
            for (int i = 0; i < nums.Length; ++i)
            {

                nums[i] += sum;
                sum = nums[i];
            }
            return nums;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            int[] nums = new int[] { 1, 2, 3, 5 };
            Solution runs = new Solution();
            runs.RunningSum(nums);
            Console.Write(nums);
            for (int i = 0; i < nums.Length; ++i)
            {
                Console.WriteLine(nums[i]);
            }

        }
    }
}
