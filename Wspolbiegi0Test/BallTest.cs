using Logic;

namespace Test
{
    public class BallTest
    {
        private Ball ball;

        [SetUp]
        public void Setup()
        {
            ball = new("1", 50, 50, 10, 10);
        }

        [Test]
        public void ConstructorTest()
        {
            Ball ball1 = new("1", 100, 200);


            Assert.That(ball1.XPosition >= 0 && ball1.XPosition <= 100);
            Assert.That(ball1.YPosition >= 0 && ball1.YPosition <= 200);

            Assert.That(ball1.XVelocity >= 5 && ball1.XVelocity <= 10);
            Assert.That(ball1.YVelocity >= 5 && ball1.YVelocity <= 10);

            Ball ball2 = new("2", 50, 50, 10, 10);
        }

        [Test]
        public void NextStateTest()
        {
            ball.NextState(100, 100);

            Assert.That(ball.XPosition == 60);
            Assert.That(ball.YPosition == 60);


            Ball ballUp = new("1", 50, 100, 0, 10);
            ballUp.NextState(100, 100);
            Assert.That(ballUp.YVelocity == -10);
            Assert.That(ballUp.YPosition == 90);

            Ball ballDown = new("2", 50, 0, 0, 10);
            ballDown.NextState(100, 100);
            Assert.That(ballDown.YVelocity == 10);
            Assert.That(ballDown.YPosition == 10);

            Ball ballLeft = new("3", 0, 50, 10, 0);
            ballLeft.NextState(100, 100);
            Assert.That(ballLeft.XVelocity == 10);
            Assert.That(ballLeft.XPosition == 10);

            Ball ballRight = new("4", 100, 50, 10, 0);
            ballRight.NextState(100, 100);
            Assert.That(ballRight.XVelocity == -10);
            Assert.That(ballRight.XPosition == 90);

            Ball ballCorner = new("5", 100, 100, 10, 10);
            ballCorner.NextState(100, 100);
            Assert.That(ballCorner.XVelocity == -10);
            Assert.That(ballCorner.YVelocity == -10);
            Assert.That(ballCorner.XPosition == 90);
            Assert.That(ballCorner.YPosition == 90);
        }


    }
}