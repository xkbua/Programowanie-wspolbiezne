using Data;
using Logic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace View
{

    public partial class MainWindow : Window
    {
        private Board board;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (board != null)
            {
                MyCanvas.Children.Clear();
                foreach (var ball in board.Balls)
                {
                    ball.BallPositionChanged -= BallPositionChangedHandler;
                }
            }
            
            int number = Convert.ToInt32(NumberTextBox.Text);
            BallLogic[] balls = new BallLogic[number];
            board = new Board(balls);
            for(int i = 0; i < number; i++) 
            {
                BallLogic ball = new BallLogic(i);
                ball.BallPositionChanged += BallPositionChangedHandler;
                balls[i] = ball;
                Ellipse ellipse = new Ellipse();
                ellipse.StrokeThickness = 1;
                ellipse.Stroke = Brushes.Black;
                ellipse.Fill = Brushes.Blue;
                ellipse.Width = 2 * ball.Radius;
                ellipse.Height = ellipse.Width;
                Canvas.SetLeft(ellipse, ball.XPosition - ball.Radius);
                Canvas.SetTop(ellipse, ball.YPosition - ball.Radius);
                MyCanvas.Children.Add(ellipse);
            }
        }

        public void BallPositionChangedHandler(object sender, BallPositionChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Canvas.SetLeft(MyCanvas.Children[e.BallId], e.NewXPosition - e.Radius);
                Canvas.SetTop(MyCanvas.Children[e.BallId], e.NewYPosition - e.Radius);
            });
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            foreach (var ball in board.Balls)
            {
                ball.BallPositionChanged -= BallPositionChangedHandler;
            }

            Application.Current.Shutdown();
        }

    }
}