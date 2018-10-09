using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int SIZE = 16;

        DispatcherTimer timer;
        Random random;

        Point snake;
        Point food;

        int width;
        int height;

        int snakeStepX;
        int snakeStepY;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            random = new Random();
            width = (int)Map.ActualWidth;
            height = (int)Map.ActualHeight;

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new System.TimeSpan(50000);
            timer.IsEnabled = true;
            AddFood();

            KeyDown += MainWindow_KeyDown;

            snake = new Point(width / 2,  height / 2);
            snakeStepX = 0;
            snakeStepY = 0;
            MoveSnake();

        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down: snakeStepX = 0; snakeStepY = +1; break;
                case Key.Up: snakeStepX = 0; snakeStepY = -1; break;
                case Key.Left: snakeStepX = -1; snakeStepY = 0; break;
                case Key.Right: snakeStepX = 1; snakeStepY = 0; break;
            }
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            MoveSnake();
        }

        void MoveSnake()
        {
            snake.X += snakeStepX;
            snake.Y += snakeStepY;

            Ellipse ellipse = CreateEllipse(snake, Brushes.HotPink);
            if (Map.Children.Count > 1)
            {
                Map.Children.RemoveAt(1);
            }
            Map.Children.Insert(1, ellipse);
        }

        void AddFood()
        {
            

            food = new Point(random.Next(width - SIZE), random.Next(height - SIZE));
            Ellipse ellipse = CreateEllipse(food, Brushes.Khaki);
            if (Map.Children.Count > 0)
            {
                Map.Children.RemoveAt(0);
            }
            Map.Children.Insert(0, ellipse);
        }

        private Ellipse CreateEllipse(Point point, Brush brush)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = SIZE,
                Height = SIZE,
                Fill = brush
            };
            Canvas.SetLeft(ellipse, point.X);
            Canvas.SetTop(ellipse, point.Y);

            return ellipse;
        }
    }
}
