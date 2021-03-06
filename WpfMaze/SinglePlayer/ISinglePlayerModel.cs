﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.ComponentModel;

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
        string Direction { get; set; }
        string GenerateMaze();
        string SolveMaze();
        string StartMaze();
        
        Position CurrPosition { get; set; }
        
       

    }
}
