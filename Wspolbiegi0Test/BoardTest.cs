using Logic;

namespace Test
{
    public class BoardTest
    {
        private BallLogic ball;

        [SetUp]
        public void Setup()
        {
            ball = new("1", 50, 50, 10, 10);
        }

        [Test]
        public void ConstructorTest()
        {
            BallLogic ball1 = new("1", 100, 200);
            BallLogic ball2 = new("2", 100, 200);

            Board board = new(100, 200, new BallLogic[] { ball1, ball2 });

            Assert.That(board.Width == 100);
            Assert.That(board.Height == 200);
            Assert.That(board.Balls.Length == 2);
            Assert.That(board.Balls[0] == ball1);
            Assert.That(board.Balls[1] == ball2);
        }

        [Test]
        public void MoveTest()
        {
            BallLogic ball1 = new("1", 50, 50, 10, 10);
            BallLogic ball2 = new("2", 50, 50, 10, 10);

            Board board = new(100, 100, new BallLogic[] { ball1, ball2 });

            board.Move();

            Assert.That(ball1.XPosition == 60);
            Assert.That(ball1.YPosition == 60);
            Assert.That(ball2.XPosition == 60);
            Assert.That(ball2.YPosition == 60);
        }

    }
}