﻿using MazeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class StartMazeCommand : ICommand
    {
        private IModel model;
        public StartMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            // get the game from the model.
            GameMultiPlayer game = model.GenerateGame(name, rows, cols, client);
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);

            if (game != null)
            {
                return "multiPlayer";
            } else
            {
                writer.WriteLine("exist game");
                writer.Flush();
                return "singlePlayer";
            }
        }
    }
}
