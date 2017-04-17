using MazeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class GenerateMazeCommand : ICommand
    {
        private IModel model;
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            string result;
            
            if (!model.ContainMaze(name))
            { 
                Maze maze = model.GenerateMaze(name, rows, cols);
                maze.Name = name;
                result = maze.ToJSON();
            }
            else
            {
                result = "error maze exist";
            }
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);           
            writer.WriteLine(result);
            writer.Flush();

            return "singlePlayer";
        }
    }
}
