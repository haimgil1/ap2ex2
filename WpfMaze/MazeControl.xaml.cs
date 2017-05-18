using MazeLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
public enum Direction { Up, Right, Down, Left }
namespace WpfMaze
{
    /// <summary>
    /// Interaction logic for MazeControl.xaml
    /// </summary>
    public partial class MazeControl : UserControl
    {
        private Grid grid;
        private double hight;
        private double width;
        private string mazeName;
        private Position initialPos;
        private Position goalPos;
        private Maze mazeFromJson;
        private double hightRect;
        private double widthRect;


        public MazeControl()
        {
            InitializeComponent();
            grid = new Grid();
            width = this.Width;
            hight = this.Height;
            Content = grid; // the content is grid.
            grid.ShowGridLines = true;
        }

        public Image PlayerImageFile
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public Image ExitImageFile
        {
            get;
            set;
        }
        public int Cols
        {
            get;
            set;
        }
        public int Rows
        {
            get;
            set;
        }
        public string MazeName
        {
            get
            {
                return this.mazeName;

            }
            set
            {
                this.mazeFromJson = Maze.FromJSON(value);
                this.mazeName = mazeFromJson.Name;
                this.DrawMaze(mazeFromJson.ToString().Replace("\r\n", ""));

            }
        }

        public Maze MazeFromJason { get; set; }
        public Position InitialPos
        {
            get
            {
                return this.initialPos;
            }
            set
            {
                //string path = "C:\Users\chene\OneDrive\Documents\Visual Studio 2015\Projects\Server\WpfMaze\minion.jpg"
                this.initialPos = value;
                this.DrawInitialPos(value, Colors.Red, 0);
            }
        }
        public Position GoalPos
        {
            get
            {
                return this.goalPos;
            }
            set
            {
                this.goalPos = value;
                this.DrawInitialPos(value, Colors.Purple, 1);
            }
        }



        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeNameD =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeControl),
                new PropertyMetadata(MazeNameChanges));

        public static readonly DependencyProperty ColsD =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeControl),
                new PropertyMetadata(ColsChanges));

        public static readonly DependencyProperty RowsD =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeControl),
                new PropertyMetadata(RowsChanges));

        public static readonly DependencyProperty InitialPosD =
            DependencyProperty.Register("InitialPos", typeof(Position), typeof(MazeControl),
                new PropertyMetadata(InitialPosChanges));

        public static readonly DependencyProperty GoalPosD =
            DependencyProperty.Register("GoalPos", typeof(Position), typeof(MazeControl),
                new PropertyMetadata(GoalPosChanges));

        private static void MazeNameChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.MazeName = (string)e.NewValue;
        }

        private static void ColsChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.Cols = (int)e.NewValue;
        }


        private static void RowsChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.Rows = (int)e.NewValue;
        }

        private static void InitialPosChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.InitialPos = (Position)e.NewValue;
        }

        private static void GoalPosChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.GoalPos = (Position)e.NewValue;
        }

        public void DrawMaze(string str)
        {
           
            this.hightRect = this.hight / Rows;
            this.widthRect = this.width / Cols;

            SetRows();
            SetCols();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Rectangle rect = this.AddRectToGrid(i, j, hightRect, widthRect);

                    int placeInString = (i * Cols) + j;
                    if (str[placeInString] == '0')
                    {
                        rect.Fill = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        rect.Fill = new SolidColorBrush(Colors.Black);
                    }
                    grid.Children.Add(rect);
                }
            }
            //  this.DrawInitialPos(hightRect, widthRect);
            // this.DrawGoalPos(hightRect, widthRect);
        }

        private void SetRows()
        {
            for (int i = 0; i < Rows; i++)
            {
                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
            }
        }

        private void SetCols()
        {
            for (int i = 0; i < Cols; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                grid.ColumnDefinitions.Add(col);
            }
        }

        private void DrawInitialPos(Position pos, Color color, int path)
        {
            double hightRect = this.hight / Rows;
            double widthRect = this.width / Cols;
            // split the string for get two strings of numbers.
            //string[] subStrings = str.Split(',');
            //string xStr = subStrings[0].Remove(0, 1);
            //string yStr = subStrings[1].Remove(subStrings[1].Length - 1, 1);
            //// convert the string to int.
            //int x = Convert.ToInt32(xStr);
            //int y = Convert.ToInt32(yStr);
            int x = pos.Row;
            int y = pos.Col;
            Rectangle rect = this.AddRectToGrid(x, y, hightRect, widthRect);
            // rect.Fill = new SolidColorBrush(color);

            if (path == 0)
                rect.Fill = new ImageBrush(new BitmapImage(new
                    Uri(@"C:\Users\חיים\Downloads\קוד של חן\Server\WpfMaze\minion.jpg", UriKind.Relative)));
            else
                rect.Fill = new ImageBrush(new BitmapImage(new
                Uri(@"C:\Users\חיים\Downloads\קוד של חן\Server\WpfMaze\exit.jpg", UriKind.Relative)));
            grid.Children.Add(rect);
        }

        //private void DrawGoalPos(double hightRect, double widthRect)
        //{

        //    int x = (int)Char.GetNumericValue(GoalPos[1]);
        //    int y = (int)Char.GetNumericValue(GoalPos[3]);
        //    Rectangle rect = this.AddRectToGrid(x, y, hightRect, widthRect);
        //    rect.Fill = new SolidColorBrush(Colors.Red);
        //}

        private Rectangle AddRectToGrid(int x, int y, double hightRect, double widthRect)
        {
            Rectangle rect = new Rectangle();
            rect.Height = hightRect;
            rect.Width = widthRect;
            Grid.SetRow(rect, x);
            Grid.SetColumn(rect, y);
            return rect;
        }

        public void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            int row = initialPos.Row, col = initialPos.Col;
            Position newPosition = new Position();

            switch (e.Key)
            {
                case Key.Down:
                    row = initialPos.Row + 1;
                    break;
                case Key.Up:
                    row = initialPos.Row - 1;
                    break;
                case Key.Left:
                    col = initialPos.Col - 1;
                    break;
                case Key.Right:
                    col = initialPos.Col + 1;
                    break;
                default:
                    break;
            }
            newPosition.Row = row;
            newPosition.Col = col;
            if (row >= 0 && row < Rows && col >= 0 && col < Cols)
            {
                int i = initialPos.Row, j = initialPos.Col;
                if (this.mazeFromJson[row, col] == CellType.Free)
                {
                    InitialPos = newPosition;
                    Rectangle rect = this.AddRectToGrid(i, j, hightRect, widthRect);
                    rect.Fill = new SolidColorBrush(Colors.White);
                    grid.Children.Add(rect); // its not a wall
                }
            }

            //void Move(object sender, KeyEventArgs e)
            //{
            //    ViewModel vm = ViewModel.Instance;
            //    switch (e.Key)
            //    {
            //        case Key.Up:
            //            vm.Move((int)Direction.UP);
            //            break;
            //        case Key.Down:
            //            vm.Move((int)Direction.DOWN);
            //            break;
            //        case Key.Right:
            //            vm.Move((int)Direction.RIGHT);
            //            break;
            //        case Key.Left:
            //            vm.Move((int)Direction.LEFT);
            //            break;
            //    }
            //    // if the player reach to the end
            //    if (vm.VM_MyRow == vm.VM_End.Row && vm.VM_MyCol == vm.VM_End.Col)
            //    {
            //        WinWindow win = new WinWindow();
            //        win.ShowDialog();
            //        // vm.EndGame();
            //        Window window = Window.GetWindow(this);
            //        window.Content = new Menu();
            //    }
            //}
        }
    }
}
