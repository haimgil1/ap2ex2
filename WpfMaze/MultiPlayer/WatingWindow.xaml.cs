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

namespace WpfMaze.MultiPlayer
{
    /// <summary>
    /// Interaction logic for WatingWindow.xaml
    /// </summary>
    public partial class WatingWindow : Window
    {

        private MultiPlayerViewModel vm;
        public WatingWindow()
        {
            
            InitializeComponent();
            vm = new MultiPlayerViewModel(SinglePlayerModel.Instance);
            this.DataContext = vm;
            //this.StartTheGame();
        }

        public void StartTheGame()
        {
            vm.VM_StartMaze();
            MultiMazeWindow multiWin = new MultiMazeWindow();
            multiWin.Show();
            this.Close();
        }
        
    }
}
