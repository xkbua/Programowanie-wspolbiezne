using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public BallLogic[] Balls { get; set; }

        public Board(BallLogic[] balls)
        {
            Width = BoardData.WIDTH;
            Height = BoardData.HEIGHT;
            Balls = balls;
        }

        
    }
}
