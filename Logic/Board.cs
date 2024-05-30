using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using Data;


namespace Logic
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public List<BallLogic> Balls { get; set; }

        private System.Threading.Timer timer;

        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public Board(List<BallLogic> balls)
        {
            Width = BoardData.WIDTH;
            Height = BoardData.HEIGHT;
            Balls = balls;
            timer = new System.Threading.Timer(LogInfo, null, 0, 1000);
        }

        public void CheckCollisions(List<BallLogic> balls)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                BallLogic ball1 = balls[i];
                for (int j = i + 1; j < balls.Count; j++)
                {
                    BallLogic ball2 = balls[j];

                    List<BallLogic> TwoBalls = [ball1, ball2];
                    lock(TwoBalls)
                    {
                        if (IsCollision(ball1, ball2))
                        {
                            CalcCollision(ball1, ball2);
                        }
                    } 
                }
            }
        }

        private async void LogInfo(object stateInfo)
        {
            var Infolog = new
            {
                LogTime = DateTime.Now,
                BallsInfo = Balls.Select(ball => new
                {
                    BallId = ball.Id,
                    XPosition = ball.XPosition,
                    YPosition = ball.YPosition,
                    XVelocity = ball.XVelocity,
                    YVelocity = ball.YVelocity,
                    Radius = ball.Radius
                }).ToList()
            };
            string jsonString = JsonSerializer.Serialize(Infolog, new JsonSerializerOptions { WriteIndented = true });
            await LogInfoAsync(jsonString);
        }

        private async Task LogInfoAsync(string log)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                using (StreamWriter sw = new StreamWriter(BallData.PATH, true))
                {
                    await sw.WriteLineAsync(log);
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public bool IsCollision(BallLogic ball1, BallLogic ball2)
        {
            double distanceX = Math.Pow(Math.Abs(ball1.XPosition - ball2.XPosition), 2);
            double distanceY = Math.Pow(Math.Abs(ball1.YPosition - ball2.YPosition), 2);
            double distance = Math.Sqrt(distanceX + distanceY);

            if (ball1.Radius + ball2.Radius >= distance)
                return true;
            else
                return false;
        }

        private void CalcCollision(BallLogic ball1, BallLogic ball2)
        {
            float relativeVelocityX = ball1.XVelocity - ball2.XVelocity;
            float relativeVelocityY = ball1.YVelocity - ball2.YVelocity;

            float collisionAngle = (float)Math.Atan2(ball2.YPosition - ball1.YPosition, ball2.XPosition - ball1.XPosition);

            float ball1VelocityInCollisionFrameX = ball1.XVelocity * (float)Math.Cos(collisionAngle) + ball1.YVelocity * (float)Math.Sin(collisionAngle);
            float ball1VelocityInCollisionFrameY = -ball1.XVelocity * (float)Math.Sin(collisionAngle) + ball1.YVelocity * (float)Math.Cos(collisionAngle);
            float ball2VelocityInCollisionFrameX = ball2.XVelocity * (float)Math.Cos(collisionAngle) + ball2.YVelocity * (float)Math.Sin(collisionAngle);
            float ball2VelocityInCollisionFrameY = -ball2.XVelocity * (float)Math.Sin(collisionAngle) + ball2.YVelocity * (float)Math.Cos(collisionAngle);

            float totalMass = ball1.Mass + ball2.Mass;
            float newVelocityBall1X = ((ball1.Mass - ball2.Mass) / totalMass) * ball1VelocityInCollisionFrameX +
                                         (2 * ball2.Mass / totalMass) * ball2VelocityInCollisionFrameX;
            float newVelocityBall2X = (2 * ball1.Mass / totalMass) * ball1VelocityInCollisionFrameX -
                                         ((ball1.Mass - ball2.Mass) / totalMass) * ball2VelocityInCollisionFrameX;

            ball1.XVelocity = newVelocityBall1X * (float)Math.Cos(collisionAngle) - ball1VelocityInCollisionFrameY * (float)Math.Sin(collisionAngle);
            ball1.YVelocity = newVelocityBall1X * (float)Math.Sin(collisionAngle) + ball1VelocityInCollisionFrameY * (float)Math.Cos(collisionAngle);
            ball2.XVelocity = newVelocityBall2X * (float)Math.Cos(collisionAngle) - ball2VelocityInCollisionFrameY * (float)Math.Sin(collisionAngle);
            ball2.YVelocity = newVelocityBall2X * (float)Math.Sin(collisionAngle) + ball2VelocityInCollisionFrameY * (float)Math.Cos(collisionAngle);

            float distance = (float)Math.Sqrt((ball2.XPosition - ball1.XPosition) * (ball2.XPosition - ball1.XPosition) +
                                              (ball2.YPosition - ball1.YPosition) * (ball2.YPosition - ball1.YPosition));

            float radiusSum = ball1.Radius + ball2.Radius;

            if (distance < radiusSum)
            {
                float overlap = radiusSum - distance;

                ball1.XPosition += overlap * (ball1.XPosition - ball2.XPosition) / distance;
                ball1.YPosition += overlap * (ball1.YPosition - ball2.YPosition) / distance;
                ball2.XPosition -= overlap * (ball1.XPosition - ball2.XPosition) / distance;
                ball2.YPosition -= overlap * (ball1.YPosition - ball2.YPosition) / distance;
            }
        }
    }
}
