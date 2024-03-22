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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board board;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20); // Ustawienie interwału odświeżania na 50 milisekund (20 FPS)
            timer.Tick += Timer_Tick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int number = Convert.ToInt32(NumberTextBox.Text);
            Ball[] balls = new Ball[number];

            for(int i = 0; i < number; i++) 
            {
                Ball ball = new Ball(Convert.ToString(number), 200, 200);
                balls[i] = ball;
            }

            board = new Board(175, 175, balls);

            // Rozpoczęcie odświeżania
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
    }
}