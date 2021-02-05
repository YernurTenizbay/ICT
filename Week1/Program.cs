using System;
using System.Text;
namespace Week1
{
    public class Solution
    {
        public string DefangIPaddr(string address)
        {
            var add = new StringBuilder(address);
            for (int i = 0; i < add.Length; ++i)
            {
                if (add[i] == '.')
                {
                    add.Remove(i, 1);
                    add.Insert(i, "[.]");
                    i += 2;
                }

            }
            return add.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {   string s = Console.ReadLine();
            Solution ip = new Solution();
            Console.WriteLine(ip.DefangIPaddr(s));

        }
    }
}
