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
        public string Id { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float XVelocity { get; set; }
        public float YVelocity { get; set; }

        // Dodaj zdarzenie do informowania o zmianie pozycji kulki
        public event EventHandler<BallPositionChangedEventArgs> BallPositionChanged;

        public BallLogic(string id)
        {
            Random rand = new Random();

            Id = id;
            XPosition = rand.Next(0, BoardData.WIDTH);
            YPosition = rand.Next(0, BoardData.HEIGHT);

            XVelocity = rand.Next(-5, 5);
            YVelocity = rand.Next(-5, 5);

            // Uruchom wątek do automatycznej aktualizacji pozycji
            Thread thread = new Thread(() => UpdatePosition());
            thread.IsBackground = true;
            thread.Start();
        }

        public BallLogic(string id, float xPosition, float yPosition, float xVelocity, float yVelocity)
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

                // Informuj o zmianie pozycji kulki
                OnBallPositionChanged(new BallPositionChangedEventArgs(Id, XPosition, YPosition));

                Thread.Sleep(BallData.THREAD_SLEEP_TIME); // Interwał czasowy - aktualizacja co 1 sekundę
            }
        }

        public void NextState()
        {
            float NextX = XPosition + XVelocity;
            float NextY = YPosition + YVelocity;

            if (NextX < 0 || NextX > BoardData.WIDTH)
            {
                XVelocity *= -1;
            }

            if (NextY < 0 || NextY > BoardData.HEIGHT)
            {
                YVelocity *= -1;
            }

            XPosition += XVelocity;
            YPosition += YVelocity;
        }
    }
}
