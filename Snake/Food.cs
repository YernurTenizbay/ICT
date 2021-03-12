using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{

  public class Food : GameObject
        {
          
            Wall wall = new Wall('#', ConsoleColor.DarkYellow, @"Levels/level1.txt");
            Random rnd = new Random();
            
            public Food(char sign, ConsoleColor color) : base(sign, color)
            {
                Point location = new Point { X = rnd.Next(1, Game.Width), Y = rnd.Next(1, Game.Height) };
                body.Add(location);
                Draw();
            }
            public void Generate()
            {
           
                int x = rnd.Next(1, 39);
                int y = rnd.Next(1, 39);
                while (wall.coor[x, y] != 0)
                {
                    x = rnd.Next(1, 39);
                    y = rnd.Next(1, 39);
                }
                body[0].X = x;
                body[0].Y = y;
            
                Draw();
            }
        }
    

}
