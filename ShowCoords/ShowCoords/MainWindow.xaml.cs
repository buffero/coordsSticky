using ShowCoords.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace ShowCoords
{ 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Topmost = true;                        
            PreviewMouseLeftButtonDown += Window_MouseDown;
            PreviewMouseMove += MainWindow_PreviewMouseMove;
            PoorMansDependencyInjection();
        }        

        public void PoorMansDependencyInjection()
        {
            var netherMultiplier = System.Configuration.ConfigurationSettings.AppSettings["NetherCoordsMultiplier"].ToString();
            var multiplier = int.Parse(netherMultiplier);
            DataContext = new CoordsViewModel(new NetherCoordsEvaluator(multiplier), new CoordsPersistanceService<CoordsItemViewModel>());
        }

        private void MainWindow_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point position = e.GetPosition(this);
                if (Math.Abs(position.X - StartPosition.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - StartPosition.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    DragMove();
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartPosition = e.GetPosition(this);             
        }

        public Point StartPosition { get; private set; }
    }
}
