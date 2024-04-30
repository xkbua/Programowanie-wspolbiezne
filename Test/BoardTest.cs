using Logic;

namespace Test
{
    public class BoardTest
    {
        private BallLogic ball;

        [SetUp]
        public void Setup()
        {
            ball = new(0);
        }

        [Test]
        public void ConstructorTest()
        {
            // Arrange
            BallLogic ball1 = new(0);
            BallLogic ball2 = new(1);

            Board board = new(new List<BallLogic> { ball1, ball2 });

            // Assert
            Assert.That(board.Width == 175);
            Assert.That(board.Height == 175);
            Assert.That(board.Balls.Count == 2);
            Assert.That(board.Balls[0] == ball1);
            Assert.That(board.Balls[1] == ball2);
        }


        [Test]
        public void TestBoardIsCollision()
        {
            // Arrange
            List<BallLogic> balls = new List<BallLogic>
            {
                new BallLogic(1, 5, 5, 50, 50, 1, 0),
                new BallLogic(2, 5, 5, 60, 50, -1, 0)
            };

            Board board = new Board(balls);

            // Act
            bool isCollision = board.IsCollision(balls[0], balls[1]);

            // Assert
            Assert.IsTrue(isCollision == true);
        }

            [Test]
        public void TestBoardCollisionDetection()
        {
            // Arrange
            List<BallLogic> balls = new List<BallLogic>
            {
                new BallLogic(1, 5, 5, 50, 50, 1, 0),
                new BallLogic(2, 5, 5, 60, 50, -1, 0)
            };

            Board board = new Board(balls);

            // Act
            board.CheckCollisions(balls);
            balls[0].NextState();
            balls[1].NextState();

            // Assert
            Assert.IsTrue(balls[0].XVelocity == -1 && balls[1].XVelocity == 1);
            Assert.That(balls[0].XPosition, Is.EqualTo(49));
            Assert.That(balls[1].XPosition, Is.EqualTo(61));
        }
    }
}