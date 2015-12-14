using System;
using System.Windows;
using System.Windows.Input;

namespace ShowCoords
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Topmost = true;            
            MouseDown += Window_MouseDown;
            DataContext = new CoordsViewModel();        
        }        

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
