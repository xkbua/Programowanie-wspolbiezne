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
            BallLogic ball1 = new("1");
            BallLogic ball2 = new("2");

            Board board = new(new BallLogic[] { ball1, ball2 });

            Assert.That(board.Width == 175);
            Assert.That(board.Height == 175);
            Assert.That(board.Balls.Length == 2);
            Assert.That(board.Balls[0] == ball1);
            Assert.That(board.Balls[1] == ball2);
        }
    }
}