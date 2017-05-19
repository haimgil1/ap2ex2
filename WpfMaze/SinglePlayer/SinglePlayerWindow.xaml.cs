using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMaze
{
    /// <summary>
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        private SinglePlayerViewModel vm;

        public SinglePlayerWindow()
        {
            InitializeComponent();
            vm = new SinglePlayerViewModel(new SinglePlayerModel());
            this.DataContext = vm;

        }
        
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            
            SingleMazeWindow singleMazeWin = new SingleMazeWindow();
            //MainWindow win = (MainWindow)Application.Current.MainWindow;
            singleMazeWin.Show();
            this.Close();
        }


        private void DetailsControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
