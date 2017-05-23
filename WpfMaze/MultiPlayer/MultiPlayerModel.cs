using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json;

namespace WpfMaze.MultiPlayer
{
    class MultiPlayerModel : IMultiPlayerModel, INotifyPropertyChanged
    {
        ClientConnect client;
        private static MultiPlayerModel instance;
        private Position direction;
        private Position currentPosition;
        private Position startPos;
        private static Mutex singletonMutex = new Mutex();

        public event PropertyChangedEventHandler PropertyChanged;

        private MultiPlayerModel()
        {
            this.client = new ClientConnect();
            this.client.playHandler += delegate (string direction)
            {
                Console.WriteLine(direction);
                //Direction = direction;
                UpdatePosition(direction);
            };
        }
        public static MultiPlayerModel Instance
        {
            get
            {
                singletonMutex.WaitOne();
                if (instance == null)
                {
                    instance = new MultiPlayerModel();
                }
                singletonMutex.ReleaseMutex();
                return instance;
            }
        }
        public string MazeName
        {
            get { return Properties.Settings.Default.MazeName; }
            set { Properties.Settings.Default.MazeName = value; }
        }
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }

        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set { Properties.Settings.Default.MazeCols = value; }
        }
        public string MazeString
        {
            get;
            set;
        }


        public Position InitialPos
        {
            get
            {
                return this.startPos;
            }
            set
            {
                this.direction = value;
                this.startPos = value;
                this.CurrPosition = value;

            }
        }
        public Position GoalPos
        {
            get;
            set;
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
                NotifyPropertyChanged("Direction");
            }
        }

        public Position CurrPosition
        {
            get
            {
                return this.currentPosition;
            }
            set
            {
                this.currentPosition = value;
                NotifyPropertyChanged("CurrPosition");
            }
        }


        public string GenerateMaze()
        {
            string mazeString = "generate " + this.MazeName + " " + this.MazeRows +
                                " " + this.MazeCols;
            return AddCommandAndGetResalut(mazeString);
        }


        public string StartMaze()
        {
            string mazeString = "start " + this.MazeName + " " + this.MazeRows +
                                " " + this.MazeCols;
            return AddCommandAndGetResalut(mazeString);
        }

        public string JoinMaze()
        {
            string mazeString = "join " + this.MazeName;
            return AddCommandAndGetResalut(mazeString);
        }

        public List<string> ListOfGames()
        {
            //this.client.Connect();
            //this.client.AddCommand("list");
            string list = AddCommandAndGetResalut("list");
            return JsonConvert.DeserializeObject<List<string>>(list);
        }

        public void Play(string move)
        {
            string playString = "play " + move;
            this.client.AddCommand(playString);
        }

        private string AddCommandAndGetResalut(string command)
        {
            this.client.Connect();
            this.client.AddCommand(command);
            return client.GetResult();
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void UpdatePosition(string str)
        {
            int row = this.Direction.Row, col = this.Direction.Col;
            Position point = new Position();
            switch (str)
            {
                case "down":
                    row++;
                    break;
                case "up":
                    row--;
                    break;
                case "left":
                    col--;
                    break;
                case "right":
                    col++;
                    break;
            }
            point.Row = row;
            point.Col = col;
            Direction = point;
        }


    }
}
