using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;


namespace Snake
{


   public class Game
    {
        Timer wormTimer = new Timer(100);
        Timer gameTimer = new Timer(1000);
        Timer foodTimer = new Timer(20);

        public int cnt = 0;
        public static int Width { get { return 40; } }
        public static int Height { get { return 40; } }

        Worm w = new Worm('o', ConsoleColor.Green);
        Food f = new Food('*', ConsoleColor.Yellow);
        Wall wall = new Wall('▄', ConsoleColor.DarkYellow, @"Levels/level1.txt");

        
        bool change = false;
        bool pause = false;
        public bool IsRunning { get; set; }
        public Game()
        {


            gameTimer.Elapsed += GameTimer_Elapsed;
            gameTimer.Start();
            wormTimer.Elapsed += Moving;
            wormTimer.Start();
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
        bool CheckCollisionWallWithWorm()
        {

            bool b = false;
            if ((w.body[0].X < 39 && w.body[0].X > 0) && (w.body[0].Y < 39 && w.body[0].Y > 0))
                if (wall.coor[w.body[0].X, w.body[0].Y] == 1) b = true;

            return b;

        }
        private void GameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Title = DateTime.Now.ToLongTimeString();
            if (CheckCollisionFoodWithWorm())
            {
                w.Increase(w.body[0]);
                f.Generate();
                cnt++;


            }



        }
        void Moving(object sender, ElapsedEventArgs e)
        {
            w.Move();
            if (CheckCollisionFoodWithWorm())
            {
                w.Increase(w.body[0]);
                f.Generate();
                cnt++;
            }
            if (CheckCollisionWallWithWorm())
            {
                wormTimer.Stop();
                IsRunning = false;
                

            }


        }



        public void KeyPressed(ConsoleKeyInfo pressedKey)
        {
            if (cnt > 3)
            {

                wall = new Wall('#', ConsoleColor.DarkYellow, @"Levels/level2.txt");

            }


            change = false;
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    w.ChangeDirection(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    w.ChangeDirection(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    w.ChangeDirection(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    w.ChangeDirection(1, 0);
                    break;
                case ConsoleKey.S:
                    w.Save("worm");
                    break;
                case ConsoleKey.L:
                    wormTimer.Stop();
                    w.Clear();
                    f = new Food('@', ConsoleColor.Yellow);
                    wall = new Wall('#', ConsoleColor.DarkYellow, @"Levels/level1.txt");
                    w = Worm.Load("worm");
                    wormTimer.Start();
                    break;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    // wormTimer.Stop();
                    break;
                case ConsoleKey.Spacebar:
                    if (!pause)
                    {
                        wormTimer.Stop();
                        pause = true;
                    }
                    else
                    {
                        wormTimer.Start();
                        pause = false;
                    }
                    break;


            }

        }

    }
}
