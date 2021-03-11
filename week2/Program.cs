using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace week2
{   class Layer
    {
        public Boolean opened2 = false;
        public Boolean opened = false;
        public long size{
            get;
            set;
            }
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
        public Layer(DirectoryInfo dir,int pos)
        {
            this.dir = dir;
            this.pos = pos;
            
            this.content = new List<FileSystemInfo>();
            content.AddRange(this.dir.GetDirectories());
            content.AddRange(this.dir.GetFiles());
        }
        public void CreateDir(string name)
        {
            
            string path = this.dir +"/"+name;

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
        public void RenameDir(string name)
        {
            if (name != String.Empty)
            {
                dir.MoveTo(name);
            }

        }
        public void Deldir()
        {
            content.Remove(dir); ;
            dir.Delete(true);
            
        }
        public string Sizedir(DirectoryInfo d)
        {   
            try
            {//long longsize= dir.EnumerateFiles().Sum(file => file.Length);
                long longsize = d.EnumerateFiles().Sum(file => file.Length);
                this.size = longsize;
                return this.size + "";
            }
            catch
            {
                return "0";
            }
        }
        public string Sizefile(FileInfo f)
        {
            try
            {
                this.size = f.Length;
                return this.size+"";
            }
            catch
            {
                return "0";
            }
        }

        public void Openfile(FileInfo f)
        {
            try
            {
                string path = @f.ToString();
                using (FileStream fs = File.OpenRead(path))
                {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        Console.WriteLine(temp.GetString(b));
                    }
                }

            }
            catch
            {
                Console.WriteLine("0");
            }
        }
        public void PrintInfo()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            int cnt = 0;
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                if (cnt == pos)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                Console.WriteLine(d.Name+"---"+ Sizedir(d)+ " bytes");
                
                cnt++;
            }
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            foreach(FileInfo f in dir.GetFiles())
            {
                if (cnt == pos)
                {
                    if (opened == true)
                        opened2 = true;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                }
                else
                {

                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                Console.WriteLine(f.Name + "---" + Sizefile(f)+ " bytes");
                if(opened2)
                    Openfile(f);
                opened2 = false;
                cnt++;

            }

        }
        public FileSystemInfo GetCurrentObject()
        {
            return content[pos];
        }
        public void SetNewPosition(int d)
        {
            if (d > 0)
            {
                pos++;
            }
            else
            {
                pos--;
            }
            if (pos >= content.Count)
            {
                pos = 0;
            }
            else if (pos < 0)
            {
                pos = content.Count - 1;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            F3();
        }

        private static void F3()
        {

            Stack<Layer> history = new Stack<Layer>();
            history.Push(new Layer(new DirectoryInfo(@"C:\"), 0));
            bool escape = false;
            while (!escape)
            {
                Console.Clear();
                history.Peek().PrintInfo();


                
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        if (history.Peek().GetCurrentObject().GetType() == typeof(DirectoryInfo))
                        {
                            history.Push(new Layer(history.Peek().GetCurrentObject() as DirectoryInfo, 0));
                        }
                        else if (history.Peek().GetCurrentObject().GetType() == typeof(FileInfo))
                        {
                            history.Peek().opened = true;
                        }
                            break;
                    case ConsoleKey.Tab:
                        string dname = Console.ReadLine();
                        history.Peek().CreateDir(dname);
                        break;
                    case ConsoleKey.Q:
                        
                        history.Peek().Deldir();
                        history.Pop();
                        break;
                    case ConsoleKey.UpArrow:
                        history.Peek().SetNewPosition(-1);
                        
                        break;
                    case ConsoleKey.DownArrow:
                        history.Peek().SetNewPosition(1);
                        
                        break;
                    case ConsoleKey.R:
                        string rname = Console.ReadLine();
                        history.Peek().RenameDir(rname);
                        break;
                    case ConsoleKey.W:
                        
                        history.Peek().opened=false;
                        break;

                    case ConsoleKey.Escape:
                        
                            history.Pop();
                        if (history.Count == 0) escape = true;
                            break;
                       
                    
                }
            }

        }
    }
}
