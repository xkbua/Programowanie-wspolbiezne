namespace Logic
{
    public class BallPositionChangedEventArgs : EventArgs
    {
        public int BallId { get; }
        public float NewXPosition { get; }
        public float NewYPosition { get; }
        public int Radius { get; }
        public int Mass { get; }


        public BallPositionChangedEventArgs(int ballId, float newXPosition, float newYPosition, int radius, int mass)
        {
            BallId = ballId;
            NewXPosition = newXPosition;
            NewYPosition = newYPosition;
            Radius = radius;
            Mass = mass;
        }
    }
}