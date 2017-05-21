using MazeLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaze
{
  public  class SinglePlayerModel : ISinglePlayerModel, INotifyPropertyChanged
    {
        ClientConnect client;
        private static SinglePlayerModel instance;
        private string direction;
        private Position currentPosition;
        private Position startPos;

        public event PropertyChangedEventHandler PropertyChanged;

        private SinglePlayerModel()
        {
            this.client = new ClientConnect();
            this.client.playHandler += delegate (string direction)
            {
                Console.WriteLine(direction);
                //Direction = direction;
                UpdateCurrentPosition(direction);
            };
        }
        public static SinglePlayerModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SinglePlayerModel();
                }
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
                this.startPos = value;
                this.CurrPosition = value;
            }
        }
        public Position GoalPos
        {
            get;
            set;
        }

        public string Direction
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
            //this.client.Connect();
            //this.client.AddCommand(mazeString);
            //string result = null;
            //result = client.GetResult();
            //return result;
            return AddCommandAndGetResalut(mazeString);
        }

        public string SolveMaze()
        {
            string solve = "solve " + this.MazeName + " " + Properties.Settings.Default.SearchAlgorithm ;
            //this.client.Connect();
            //this.client.AddCommand(solve);
            //string result = null;
            //result = client.GetResult();
            //return result;
            return AddCommandAndGetResalut(solve);

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
            string list = AddCommandAndGetResalut("list");
            List<string> listByJson = JsonConvert.DeserializeObject<List<string>>(list);
            return listByJson;
        }

        public void Play(string move)
        {
            string playString = "play " + move;
            this.client.AddCommand(playString);
            //client.GetResult();
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


        private void UpdateCurrentPosition(string direction)
        {
            int row = this.CurrPosition.Row, col = this.CurrPosition.Col;
            Position newPosition = new Position();

            switch (direction)
            {
                case "down":
                    row = this.CurrPosition.Row + 1;
                    break;
                case "up":
                    row = this.CurrPosition.Row - 1;
                    break;
                case "left":
                    col = this.CurrPosition.Col - 1;
                    break;
                case "right":
                    col = this.CurrPosition.Col + 1;
                    break;
                default:
                    break;
            }
            newPosition.Row = row;
            newPosition.Col = col;
            this.CurrPosition = newPosition;
        }

        //public void CloseMaze()
        //{
        //    string solve = "close " + this.MazeName;
        //    this.client.Connect();
        //    this.client.AddCommand(solve);

        //}

    }
}
