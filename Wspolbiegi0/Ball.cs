using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Ball

    {
        public string Id { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float XVelocity { get; set; }
        public float YVelocity { get; set; }

        public Ball(string id, int maxXPosition, int maxYPosition)
        {
            Random rand = new Random();

            Id = id;
            XPosition = rand.Next(0, maxXPosition);
            YPosition = rand.Next(0, maxYPosition);

            XVelocity = rand.Next(0, 5);
            YVelocity = rand.Next(0, 5);
        }

        public Ball(string id, float xPosition, float yPosition, float xVelocity, float yVelocity)
        {
            Id = id;
            XPosition = xPosition;
            YPosition = yPosition;
            XVelocity = xVelocity;
            YVelocity = yVelocity;
        }

        public void NextState(int maxXPosition, int maxYPosition)
        {
            float NextX = XPosition + XVelocity;
            float NextY = YPosition + YVelocity;

            if(NextX < 0 || NextX > maxXPosition)
            {
                XVelocity *= -1;
            }

            if (NextY < 0 || NextY > maxYPosition)
            {
                YVelocity *= -1;
            }

            XPosition += XVelocity;
            YPosition += YVelocity;
        }

    }

}
