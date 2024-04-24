using Logic;

using Data;

namespace Test
{
    public class BallTest
    {
        private BallLogic ball;

        [SetUp]
        public void Setup()
        {
            ball = new(1, 50, 50, 10, 10);
        }

        [Test]
        public void ConstructorTest()
        {
            BallLogic ball1 = new(1);


            Assert.That(ball1.XPosition >= 0 && ball1.XPosition <= BoardData.WIDTH);
            Assert.That(ball1.YPosition >= 0 && ball1.YPosition <= BoardData.HEIGHT);

            Assert.That(ball1.XVelocity >= -5 && ball1.XVelocity <= 5);
            Assert.That(ball1.YVelocity >= -5 && ball1.YVelocity <= 5);

            BallLogic ball2 = new(2, 50, 50, 10, 10);
            Assert.That(ball2.XPosition, Is.EqualTo(50));
            Assert.That(ball2.YPosition, Is.EqualTo(50));
            Assert.That(ball2.XVelocity, Is.EqualTo(10));
            Assert.That(ball2.YVelocity, Is.EqualTo(10));
        }

        [Test]
        public void NextStateTest()
        {
            ball.NextState();

            Assert.That(ball.XPosition == 60);
            Assert.That(ball.YPosition == 60);

            BallLogic ballUp = new(1, 50, 175, 0, 10);
            ballUp.NextState();
            Assert.That(ballUp.YVelocity == -10);
            Assert.That(ballUp.YPosition == 165);

            BallLogic ballDown = new(2, 50, 0, 0, 10);
            ballDown.NextState();
            Assert.That(ballDown.YVelocity == 10);
            Assert.That(ballDown.YPosition == 10);

            BallLogic ballLeft = new(3, 0, 50, 10, 0);
            ballLeft.NextState();
            Assert.That(ballLeft.XVelocity == 10);
            Assert.That(ballLeft.XPosition == 10);

            BallLogic ballRight = new(4, 175, 50, 10, 0);
            ballRight.NextState();
            Assert.That(ballRight.XVelocity == -10);
            Assert.That(ballRight.XPosition == 165);

            BallLogic ballCorner = new(5, 175, 175, 10, 10);
            ballCorner.NextState();
            Assert.That(ballCorner.XVelocity == -10);
            Assert.That(ballCorner.YVelocity == -10);
            Assert.That(ballCorner.XPosition == 165);
            Assert.That(ballCorner.YPosition == 165);
        }


    }
}