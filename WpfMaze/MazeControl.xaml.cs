using MazeLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

public enum Direction { Up, Right, Down, Left }
namespace WpfMaze
{
    /// <summary>
    /// Interaction logic for MazeControl.xaml
    /// </summary>
    public partial class MazeControl : UserControl
    {
        public delegate void FinishedMazeAnimation();
        public delegate void MoveDirection(string direction);

        public event MoveDirection MoveDirectionEvent;
        public event FinishedMazeAnimation FinishedMazeAnimationEvent;

        private Grid grid;
        private double hight;
        private double width;
        private string mazeName;
        private Position initialPos;
        private Position goalPos;
        private Position currPosition;
        private Maze mazeFromJson;
        private double hightRect;
        private double widthRect;
        private string mazeString;
        private Position direction;

        public MazeControl()
        {
            InitializeComponent();
            grid = new Grid();
            width = this.Width;
            hight = this.Height;
            Content = grid; // the content is grid.
            grid.ShowGridLines = true;
        }


        public Image PlayerImageFile { get; set; }
        public Image ExitImageFile { get; set; }
        public int Cols { get; set; }
        public int Rows { get; set; }
        public double HightRect { get; set; }
        public double WidthRect { get; set; }
        public Grid Grid { get; set; }

        public Maze MazeFromJson
        {
            get { return this.mazeFromJson; }
            set { this.mazeFromJson = value; }
        }
        public string MazeName
        {
            get;
            set;
        }
        public string MazeString
        {
            get
            {
                return this.mazeString;
            }
            set
            {
                MazeFromJson = Maze.FromJSON(value);
                this.mazeName = mazeFromJson.Name;
                this.DrawMaze(mazeFromJson.ToString().Replace("\r\n", ""));

            }
        }

        public Position Direction
        {
            get
            {
                return this.direction;
            }
            set
            {

                this.direction = value;
                this.Move(value);
                this.DrawInitialPos(value, 2);
            }
        }


        public Position InitialPos
        {
            get { return this.initialPos; }
            set
            {
                this.initialPos = value;
                CurrPosition = this.initialPos;
            }

        }
        public Position CurrPosition
        {
            get
            {
                return this.currPosition;
            }
            set
            {
                this.currPosition = value;
                this.DrawInitialPos(value, 0);
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
                this.DrawInitialPos(value, 1);
            }
        }



        // Using a DependencyProperty as the backing store for Maze. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeNameD =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeControl),
                new PropertyMetadata(MazeNameChanges));

        public static readonly DependencyProperty MazeStringD =
         DependencyProperty.Register("MazeString", typeof(string), typeof(MazeControl),
             new PropertyMetadata(MazeStringChanges));

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

        public static readonly DependencyProperty CurrPositionD =
            DependencyProperty.Register("CurrPosition", typeof(Position), typeof(MazeControl),
        new PropertyMetadata(CurrPositionChanges));

        public static readonly DependencyProperty DirectionD =
            DependencyProperty.Register("Direction", typeof(Position), typeof(MazeControl),
        new PropertyMetadata(DirectionChanges));

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

        private static void MazeStringChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.MazeString = (string)e.NewValue;
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


        private static void CurrPositionChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.CurrPosition = (Position)e.NewValue;
        }

        private static void DirectionChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.Direction = (Position)e.NewValue;
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
                    Rectangle rect = this.GetRectToGrid(i, j);

                    int placeInString = (i * Cols) + j;
                    if (str[placeInString] == '1')
                    {
                        rect.Fill = new SolidColorBrush(Colors.Black);
                    }
                    grid.Children.Add(rect);
                }
            }
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

        private void DrawInitialPos(Position pos, int flag)
        {
            int x = pos.Row;
            int y = pos.Col;
            Rectangle rect = this.GetRectToGrid(x, y);

            if (flag == 0)
            {
                rect.Fill = MinyonImage;
            }
            else if (flag == 1)
            {
                rect.Fill = ExitImage;
            }
            
            else
            {
                rect.Fill = SecondPlayer;
            }

            grid.Children.Add(rect);
        }

        //private void DrawGoalPos(double hightRect, double widthRect)
        //{

        //    int x = (int)Char.GetNumericValue(GoalPos[1]);
        //    int y = (int)Char.GetNumericValue(GoalPos[3]);
        //    Rectangle rect = this.GetRectToGrid(x, y, hightRect, widthRect);
        //    rect.Fill = new SolidColorBrush(Colors.Red);
        //}

        public Rectangle GetRectToGrid(int x, int y)
        {

            Rectangle rect = new Rectangle();
            rect.Height = this.hightRect;
            rect.Width = this.widthRect;
            Grid.SetRow(rect, x);
            Grid.SetColumn(rect, y);
            return rect;

        }
        public void AddRectToGrid(int i, int j)
        {
            Rectangle rect = this.GetRectToGrid(i, j);
            rect.Fill = new SolidColorBrush(Colors.White);
            grid.Children.Add(rect);
        }

        public void SolvingMaze(string solution)
        {
            int row = CurrPosition.Row, col = CurrPosition.Col;
            Position newPosition = new Position();
            foreach (char c in solution)
            {
                switch (c - '0')
                {
                    case 0:
                        col = CurrPosition.Col + 1;
                        break;
                    case 1:
                        col = CurrPosition.Col - 1;
                        break;
                    case 2:
                        row = CurrPosition.Row + 1;
                        break;
                    case 3:
                        row = CurrPosition.Row - 1;
                        break;
                    default:
                        break;
                }

                newPosition.Row = row;
                newPosition.Col = col;
                int i = CurrPosition.Row, j = CurrPosition.Col;
                Dispatcher.Invoke((Action)delegate
                {
                    CurrPosition = newPosition;
                    AddRectToGrid(i, j);

                });


                System.Threading.Thread.Sleep(300);
            }

            Dispatcher.Invoke((Action)delegate
            {
                this.RestartTheGame();

            });

        }


        public void RestartTheGame()
        {
            int i = CurrPosition.Row;
            int j = CurrPosition.Col;
            AddRectToGrid(i, j);
            CurrPosition = InitialPos;
            GoalPos = GoalPos;
        }


        public void Move(Position position)
        {

            int i = this.CurrPosition.Row, j = this.CurrPosition.Col;
            this.CurrPosition = position;
            this.AddRectToGrid(i, j);

            //if (this.GoalPos.Row == position.Row && this.GoalPos.Col == position.Col)
            //{
                //todo

                //OtherPlayerWinWindow winWindow = new OtherPlayerWinWindow();
                //winWindow.ShowDialog();
                //if (winWindow.Resualt)
                //{
                //    FinishedMazeAnimationEvent?.Invoke();
                //}
                // the other client win.
            //}
        }



        public void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            int row = CurrPosition.Row, col = CurrPosition.Col;
            Position newPosition = new Position();
            string move = null;

            switch (e.Key)
            {
                case Key.Down:
                    row = CurrPosition.Row + 1;
                    move = "down";
                    break;
                case Key.Up:
                    row = CurrPosition.Row - 1;
                    move = "up";
                    break;
                case Key.Left:
                    col = CurrPosition.Col - 1;
                    move = "left";
                    break;
                case Key.Right:
                    col = CurrPosition.Col + 1;
                    move = "right";
                    break;
                default:
                    break;
            }

            newPosition.Row = row;
            newPosition.Col = col;
            if (row >= 0 && row < Rows && col >= 0 && col < Cols)
            {
                int i = CurrPosition.Row, j = CurrPosition.Col;
                if (MazeFromJson[row, col] == CellType.Free)
                {
                    MoveDirectionEvent?.Invoke(move);

                    CurrPosition = newPosition;
                    AddRectToGrid(i, j);

                }

                if (GoalPos.Row == row && GoalPos.Col == col)
                {
                    WinWindow winWindow = new WinWindow();
                    winWindow.ShowDialog();
                    FinishedMazeAnimationEvent?.Invoke();

                }
            }
        }
    }
}
