using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaze
{
    class SinglePlayerModel : ISinglePlayerModel
    {
        ClientConnect client;
        public SinglePlayerModel()
        {
            client = new ClientConnect();
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
            get;
            set;
        }
        public Position GoalPos
        {
            get;
            set;
        }

        public string GenerateMaze()
        {
            string mazeString = "generate" + " " + this.MazeName + " " + this.MazeRows +
                " " + this.MazeCols;
            this.client.Connect();
            this.client.AddCommand(mazeString);
            string result = null;
            result = client.GetResult();

            return result;
        }

    }
}
