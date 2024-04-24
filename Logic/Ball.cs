using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class BallLogic

    {
        public int Id { get; set; }
        public int Mass { get; set; }
        public int Radius { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float XVelocity { get; set; }
        public float YVelocity { get; set; }

        public event EventHandler<BallPositionChangedEventArgs> BallPositionChanged;

        public BallLogic(int id)
        {
            Random rand = new Random();

            Id = id;
            
            int modifier = rand.Next(2, 8);
            Radius = modifier * BallData.RADIUS;
            Mass = modifier * BallData.MASS;

            XPosition = rand.Next(0 + Radius, BoardData.WIDTH - Radius);
            YPosition = rand.Next(0 + Radius, BoardData.HEIGHT - Radius);

            XVelocity = rand.Next(-5, 5);
            YVelocity = rand.Next(-5, 5);

            Thread thread = new Thread(() => UpdatePosition());
            thread.IsBackground = true;
            thread.Start();
        }

        public BallLogic(int id, float xPosition, float yPosition, float xVelocity, float yVelocity)
        {
            Id = id;
            XPosition = xPosition;
            YPosition = yPosition;
            XVelocity = xVelocity;
            YVelocity = yVelocity;
        }

        protected virtual void OnBallPositionChanged(BallPositionChangedEventArgs e)
        {
            BallPositionChanged?.Invoke(this, e);
        }

        private void UpdatePosition()
        {
            while (true)
            {
                NextState();

                OnBallPositionChanged(new BallPositionChangedEventArgs(Id, XPosition, YPosition, Radius, Mass));

                Thread.Sleep(BallData.THREAD_SLEEP_TIME);
            }
        }

        public void NextState()
        {
            float NextX = XPosition + XVelocity;
            float NextY = YPosition + YVelocity;

            if (NextX < 0 + Radius || NextX > BoardData.WIDTH - Radius)
            {
                XVelocity *= -1;
            }

            if (NextY < 0 + Radius || NextY > BoardData.HEIGHT - Radius)
            {
                YVelocity *= -1;
            }

            XPosition += XVelocity;
            YPosition += YVelocity;
        }
    }
}
