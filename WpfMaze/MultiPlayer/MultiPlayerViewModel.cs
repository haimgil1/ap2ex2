using MazeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaze.MultiPlayer
{
    class MultiPlayerViewModel : ViewModel
    {
        private IMultiPlayerModel model;

        public MultiPlayerViewModel(IMultiPlayerModel model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
            NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }


        //public ObservableCollection<string> VM_list
        //{
        //    set
        //    {
        //        NotifyPropertyChanged("VM_list");
        //    }
        //}
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

        public Position VM_Direction
        {
            get { return model.Direction; }
            set
            {
                model.Direction = value;
            }
        }

        public Position VM_CurrPosition
        {
            get { return model.CurrPosition; }
            set
            {
                model.CurrPosition = value;
            }
        }


        public List<string> VM_ListOfGames()
        {
            return model.ListOfGames();
           
        }

        public void VM_StartMaze()
        {
            string str = model.StartMaze();
            Maze maze = Maze.FromJSON(str);

            VM_MazeString = str;
            VM_MazeRows = maze.Rows;
            VM_MazeCols = maze.Cols;
            VM_MazeName = maze.Name;
            VM_InitialPos = maze.InitialPos;
            VM_GoalPos = maze.GoalPos;
        }

        public void VM_JoinMaze()
        {
            string str = model.JoinMaze();
            Maze maze = Maze.FromJSON(str);

            VM_MazeName = maze.Name;
            VM_MazeRows = maze.Rows;
            VM_MazeCols = maze.Cols;
            VM_MazeString = str;
            VM_InitialPos = maze.InitialPos;
            VM_GoalPos = maze.GoalPos;
        }

        public void VM_Play(string move)
        {
            model.Play(move);
        }

    }
}
