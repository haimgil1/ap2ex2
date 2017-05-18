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
    /// Interaction logic for SingleMazeWindow.xaml
    /// </summary>
    public partial class SingleMazeWindow : Window
    {
        private SinglePlayerViewModel vm;

        public SingleMazeWindow()
        {
            InitializeComponent();
            vm = new SinglePlayerViewModel(new SinglePlayerModel());
            this.DataContext = vm;
            vm.VM_GenerateMaze();
            this.KeyDown += new System.Windows.Input.KeyEventHandler(grid.Grid_KeyDown);
        }


        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            // MainWindow win =  new MainWindow();

            MainWindow win = (MainWindow)Application.Current.MainWindow;

            win.Show();
            this.Close();
        }


    }
}
