using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public BallLogic[] Balls { get; set; }

        public Board(int width, int height, BallLogic[] balls)
        {
            Width = width;
            Height = height;
            Balls = balls;
        }

        public void Move()
        {
            for (int i = 0; i < Balls.Length; i++)
            {
                Balls[i].NextState(Width, Height);
            }
        }


    }
}
