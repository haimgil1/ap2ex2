﻿using MazeLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfMaze
{
  public  class SinglePlayerModel : ISinglePlayerModel
    {
        ClientConnect client;
        private static SinglePlayerModel instance;
        private string direction;
        private Position currentPosition;
        private Position startPos;
        private static Mutex singletonMutex = new Mutex();

        private SinglePlayerModel()
        {
            this.client = new ClientConnect();
            
        }

        public static SinglePlayerModel Instance
        {
            get
            {
                singletonMutex.WaitOne();
                if (instance == null)
                {
                    instance = new SinglePlayerModel();
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
                
            }
        }


        public string GenerateMaze()
        {
            //this.client.Connect();
            string mazeString = "generate " + this.MazeName + " " + this.MazeRows +
                " " + this.MazeCols;
       
            return AddCommandAndGetResalut(mazeString);
        }

        public string SolveMaze()
        {
            string solve = "solve " + this.MazeName + " " + Properties.Settings.Default.SearchAlgorithm ;
    
            return AddCommandAndGetResalut(solve);

        }

        public string StartMaze()
        {
            
            string mazeString = "start " + this.MazeName + " " + this.MazeRows +
                " " + this.MazeCols;
            return AddCommandAndGetResalut(mazeString);
        }

        
        private string AddCommandAndGetResalut(string command)
        {
            this.client.Connect();
            this.client.AddCommand(command);
            return client.GetResult();
        }

      

    }
}
