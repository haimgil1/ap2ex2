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
using MazeLib;
using Newtonsoft.Json.Linq;

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
            this.KeyDown += new System.Windows.Input.KeyEventHandler(Grid_KeyDown);
        }


        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            WarningWindow endGame = new WarningWindow();
            endGame.ShowDialog();
            if (endGame.Resualt)
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
               
            }
            
        }


        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            WarningWindow endGame = new WarningWindow();
            endGame.ShowDialog();
            if (endGame.Resualt)
            {
                int i = mazeControl.CurrPosition.Row;
                int j = mazeControl.CurrPosition.Col;
                mazeControl.AddRectToGrid(i,j);
                mazeControl.CurrPosition = mazeControl.InitialPos;
                mazeControl.GoalPos = mazeControl.GoalPos;
            }

        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            mazeControl.CurrPosition = mazeControl.InitialPos;
            string jsonSolution = this.vm.VM_SolveMaze();
            string solution = (string)JObject.Parse(jsonSolution)["Solution"];
            Task task = new Task(() =>
            {
                mazeControl.SolvingMaze(solution);
            });
            task.Start();
        }


        public void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            int row = mazeControl.CurrPosition.Row, col = mazeControl.CurrPosition.Col;
            Position newPosition = new Position();

            switch (e.Key)
            {
                case Key.Down:
                    row = mazeControl.CurrPosition.Row + 1;
                    break;
                case Key.Up:
                    row = mazeControl.CurrPosition.Row - 1;
                    break;
                case Key.Left:
                    col = mazeControl.CurrPosition.Col - 1;
                    break;
                case Key.Right:
                    col = mazeControl.CurrPosition.Col + 1;
                    break;
                default:
                    break;
            }
            newPosition.Row = row;
            newPosition.Col = col;
            if (row >= 0 && row < mazeControl.Rows && col >= 0 && col < mazeControl.Cols)
            {
                int i = mazeControl.CurrPosition.Row, j = mazeControl.CurrPosition.Col;
                if (mazeControl.MazeFromJson[row, col] == CellType.Free)
                {
                    mazeControl.CurrPosition = newPosition;
                    mazeControl.AddRectToGrid(i, j);

                }

                if (mazeControl.GoalPos.Row == row && mazeControl.GoalPos.Col == col)
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
