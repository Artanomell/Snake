using System;
using System.Windows;
using System.Windows.Controls;
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

        Point food;

        int width;
        int height;

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
            timer.Interval = new System.TimeSpan(5000000);
            timer.IsEnabled = true;
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            AddFood();
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
