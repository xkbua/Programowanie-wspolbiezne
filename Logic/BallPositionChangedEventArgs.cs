namespace Logic
{
    public class BallPositionChangedEventArgs : EventArgs
    {
        public string BallId { get; }
        public float NewXPosition { get; }
        public float NewYPosition { get; }

        public BallPositionChangedEventArgs(string ballId, float newXPosition, float newYPosition)
        {
            BallId = ballId;
            NewXPosition = newXPosition;
            NewYPosition = newYPosition;
        }
    }
}