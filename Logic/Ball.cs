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
            
            int modifier = rand.Next(3, 8);
            Radius = modifier * BallData.RADIUS;
            Mass = (int) (Math.PI * Radius * Radius);

            XPosition = rand.Next(Radius, BoardData.WIDTH - Radius);
            YPosition = rand.Next(Radius, BoardData.HEIGHT - Radius);

            XVelocity = (float) rand.NextDouble() * rand.Next(2) * 2 - 1;
            YVelocity = (float) rand.NextDouble() * rand.Next(2) * 2 - 1;

            Thread thread = new Thread(() => UpdatePosition());
            thread.IsBackground = true;
            thread.Start();
        }

        public BallLogic(int id, int mass, int radius, float xPosition, float yPosition, float xVelocity, float yVelocity)
        {
            Id = id;
            Mass = mass;
            Radius = radius;
            XPosition = xPosition;
            YPosition = yPosition;
            XVelocity = xVelocity;
            YVelocity = yVelocity;
        }

        protected virtual void OnBallPositionChanged(BallPositionChangedEventArgs e)
        {
            BallPositionChanged?.Invoke(this, e);
        }

        private async Task UpdatePosition()
        {
            while (true)
            {
                await Task.Delay(BallData.THREAD_SLEEP_TIME);

                NextState();

                OnBallPositionChanged(new BallPositionChangedEventArgs(Id, XPosition, YPosition, Radius, Mass));
            }
        }

        public void NextState()
        {
            float NextX = XPosition + XVelocity;
            float NextY = YPosition + YVelocity;

            if (NextX <= Radius)
            {
                XVelocity *= -1;
                XPosition = Radius;
            }

            if (NextX >= BoardData.WIDTH - Radius)
            {
                XVelocity *= -1;
                XPosition = BoardData.WIDTH - Radius;
            }

            if (NextY <= Radius)
            {
                YVelocity *= -1;
                YPosition = Radius;
            }

            if (NextY >= BoardData.HEIGHT - Radius)
            {
                YVelocity *= -1;
                YPosition = BoardData.HEIGHT - Radius;
            }

            XPosition += XVelocity;
            YPosition += YVelocity;
        }
    }
}
