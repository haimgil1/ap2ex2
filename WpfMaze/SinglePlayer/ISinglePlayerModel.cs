using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace WpfMaze
{
    interface ISinglePlayerModel
    {
        string MazeName { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }
        string MazeString { get; set; }
        Position InitialPos { get; set; }
        Position GoalPos { get; set; }
        string GenerateMaze();
        string SolveMaze();
        //void CloseMaze();
    }
}
