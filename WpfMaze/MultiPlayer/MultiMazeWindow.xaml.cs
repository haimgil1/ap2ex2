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
            vm = new MultiPlayerViewModel(SinglePlayerModel.Instance);
            this.DataContext = vm;
            //this.StartTheGame();
            this.KeyDown += new System.Windows.Input.KeyEventHandler(Grid_KeyDown);
        }

        public void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            int row = mazeControl1.CurrPosition.Row, col = mazeControl1.CurrPosition.Col;
            Position newPosition = new Position();

            switch (e.Key)
            {
                case Key.Down:
                    row = mazeControl1.CurrPosition.Row + 1;
                    vm.VM_Play("down");
                    break;
                case Key.Up:
                    row = mazeControl1.CurrPosition.Row - 1;
                    vm.VM_Play("up");
                    break;
                case Key.Left:
                    col = mazeControl1.CurrPosition.Col - 1;
                    vm.VM_Play("left");
                    break;
                case Key.Right:
                    col = mazeControl1.CurrPosition.Col + 1;
                    vm.VM_Play("right");
                    break;
                default:
                    break;
            }
            newPosition.Row = row;
            newPosition.Col = col;
            if (row >= 0 && row < mazeControl1.Rows && col >= 0 && col < mazeControl1.Cols)
            {
                int i = mazeControl1.CurrPosition.Row, j = mazeControl1.CurrPosition.Col;
                if (mazeControl1.MazeFromJson[row, col] == CellType.Free)
                {
                    mazeControl1.CurrPosition = newPosition;
                    mazeControl1.AddRectToGrid(i, j);

                }

                if (mazeControl1.GoalPos.Row == row && mazeControl1.GoalPos.Col == col)
                {
                    WinWindow winWindow = new WinWindow();
                    winWindow.ShowDialog();
                    if (winWindow.Resualt)
                    {

                        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                        mainWindow.Show();
                        this.Close();
                    }
                }
            }
        }



    }
}
