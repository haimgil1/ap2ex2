using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
     public class SolveMazeCommand : ICommand
    {
        private IModel model;
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            string typeAlgorithem = args[1];
            string result;
            AdapterSolution adpterSolution = null;
            Solution<Position> s = null;

            switch (typeAlgorithem)
            {
                case "0":
                    s = model.solveMazeBFS(name);
                    break;
                case "1":
                    s = model.solveMazeDFS(name);
                    break;
            }
            if (s != null)
            {
                adpterSolution = new AdapterSolution(s, name, s.SolutionSize());
                result = adpterSolution.ToJson();
            } else
            {
                result = "error exist maze";
            }

            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(result);
            writer.Flush();

            return "singlePlayer";

        }
    }
}
