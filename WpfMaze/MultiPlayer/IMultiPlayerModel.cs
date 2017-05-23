using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace WpfMaze.MultiPlayer
{
    
   

    interface IMultiPlayerModel
    {
        string MazeName { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }
        string MazeString { get; set; }
        Position InitialPos { get; set; }
        Position GoalPos { get; set; }
        Position CurrPosition { get; set; }
        Position Direction { get; set; }
        string GenerateMaze();
        string StartMaze();
        string JoinMaze();
        void Play(string move);
        List<string> ListOfGames();
        event PropertyChangedEventHandler PropertyChanged;

    }
}
