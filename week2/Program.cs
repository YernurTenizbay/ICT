using System;
using System.IO;
using System.Collections.Generic;
namespace week2
{   class Layer
    {
        public DirectoryInfo dir
        {
            get;
            set;
        }
        public int pos
        {
            get;
            set;
        }
        public List<FileSystemInfo> content
        {
            get;
            set;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            F3();
        }
        private static void PrintInfo(string path,int pos)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            int cnt = 0;
            foreach(DirectoryInfo d in dir.GetDirectory())
            {

            }
          
        }
        private static void F3()
        {
            Stack<Layer> history = new Stack<Layer>();
            history.Push(new Layer { dir = new DirectoryInfo(@"C:\"), pos = 0 });
            bool escape = false;
            List<FileSystemInfo> fileSystemInfos = new List<FileSystemInfo>();
        }
    }
}
