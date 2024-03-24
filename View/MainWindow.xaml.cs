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

            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(20); 
            timer.Tick += Timer_Tick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int number = Convert.ToInt32(NumberTextBox.Text);
            BallLogic[] balls = new BallLogic[number];

            for(int i = 0; i < number; i++) 
            {
                BallLogic ball = new BallLogic(Convert.ToString(number), 175, 175);
                balls[i] = ball;
            }

            board = new Board(175, 175, balls);

            timer.Start();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            board.Move();

            DrawBalls();
        }

        private void DrawBalls()
        {

            MyCanvas.Children.Clear();

            foreach (var ball in board.Balls)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = Brushes.Blue;
                ellipse.Width = 10;
                ellipse.Height = 10;
                Canvas.SetLeft(ellipse, ball.XPosition);
                Canvas.SetTop(ellipse, ball.YPosition);
                MyCanvas.Children.Add(ellipse);
            }
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}