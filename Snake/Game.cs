using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;


namespace Snake
{


    class Game
    {
        Timer foodTimer = new Timer(20);
        public int cnt = 0;
        public static int Width { get { return 40; } }
        public static int Height { get { return 40; } }
        
        Worm w = new Worm('o', ConsoleColor.Green);
        Food f = new Food('*', ConsoleColor.Yellow);
        Wall wall = new Wall('▄', ConsoleColor.DarkYellow, @"Levels/Level1.txt");
        
        public bool IsRunning { get; set; }
        public Game()
        {
            foodTimer.Elapsed += GameTimer_Elapsed;
            foodTimer.Start();
            IsRunning = true;
            Console.CursorVisible = false;
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width, Height);
        }


        bool CheckCollisionFoodWithWorm()
        {
            return w.body[0].X == f.body[0].X && w.body[0].Y == f.body[0].Y;
        }
        private void GameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (CheckCollisionFoodWithWorm())
            {
                w.Increase(w.body[0]);
                f.Generate();
                cnt++;


            }
        }
        bool CheckCollisionWallWithWorm()
        { 
            bool b = false;
            if((w.body[0].X<39 && w.body[0].X>0)&& (w.body[0].Y < 39 && w.body[0].Y > 0))
                if (wall.coor[w.body[0].X, w.body[0].Y] == 1) b = true;
            return b;
        }


        public void KeyPressed(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    w.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    w.Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    w.Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    w.Move(1, 0);
                    break;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    break;
            }


            if (cnt > 3)
            {
               
                
                Wall wall = new Wall('▄', ConsoleColor.Yellow, @"Levels/Level2.txt");
                


            }

            if (CheckCollisionWallWithWorm())
            {
                
                IsRunning = false;
                
            }
        }

    }
    
}
