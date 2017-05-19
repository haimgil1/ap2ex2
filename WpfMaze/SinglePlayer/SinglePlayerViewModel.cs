using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaze
{
    class SinglePlayerViewModel : ViewModel
    {
        private ISinglePlayerModel model;

        public SinglePlayerViewModel(ISinglePlayerModel model)
        {
            this.model = model;
        }

        public string VM_MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;
                NotifyPropertyChanged("VM_MazeName");
            }
        }

        public int VM_MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("VM_MazeRows");
            }
        }

        public int VM_MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("VM_MazeCols");
            }
        }
        public string VM_MazeString
        {
            get { return model.MazeString; }
            set
            {
                model.MazeString = value;
                NotifyPropertyChanged("VM_MazeString");
            }
        }

        public Position VM_InitialPos
        {
            get { return model.InitialPos; }
            set
            {
                model.InitialPos = value;
                NotifyPropertyChanged("VM_InitialPos");
            }
        }

        public Position VM_GoalPos
        {
            get { return model.GoalPos; }
            set
            {
                model.GoalPos = value;
                NotifyPropertyChanged("VM_GoalPos");
            }
        }


        public void VM_GenerateMaze()
        {
            string str = model.GenerateMaze();
            Maze maze = Maze.FromJSON(str);
           
            VM_MazeName = maze.Name;
            VM_MazeRows = maze.Rows;
            VM_MazeCols = maze.Cols;
            VM_MazeString = str;
            VM_InitialPos = maze.InitialPos;
            VM_GoalPos = maze.GoalPos;
        }

        public string VM_SolveMaze()
        {
            return this.model.SolveMaze();
        }




    }
}

