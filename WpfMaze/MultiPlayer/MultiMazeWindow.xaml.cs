using MazeLib;
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
    /// Interaction logic for MultiMazeWindow.xaml
    /// </summary>
    public partial class MultiMazeWindow : Window
    {
        private MultiPlayerViewModel vm;

        public MultiMazeWindow()
        {
            InitializeComponent();
            vm = new MultiPlayerViewModel(MultiPlayerModel.Instance);
            this.DataContext = vm;
            //this.StartTheGame();
            this.KeyDown += new System.Windows.Input.KeyEventHandler(MazeControl1.Grid_KeyDown);
            //this.KeyDown += new System.Windows.Input.KeyEventHandler(Grid_KeyDown);
            this.MazeControl1.FinishedMazeAnimationEvent += FinishAnimation;
            this.MazeControl1.MoveDirectionEvent += MoveDirection;
        }


        public void FinishAnimation()
        {
            MainWindow mainWindow = new MainWindow();
           // MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Show();
            this.Close();

        }

        public void MoveDirection(string str)
        {
            vm.VM_Play(str);
        }
    }
}
