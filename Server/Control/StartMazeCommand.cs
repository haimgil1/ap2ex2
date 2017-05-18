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
    /// <summary>
    /// Class : StartMazeCommand. The class responsible to start a new game.
    /// If the game exist the client gets a message.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class StartMazeCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="StartMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public StartMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            if (!this.CheckValid(args, client))
            {
                return "multiPlayer";
            }
		    model.GetmodelData().mutexGamePlaying.WaitOne();
		    model.GetmodelData().mutexGameWating.WaitOne();
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            // Get the game from the model.
            GameMultiPlayer game = model.GenerateGame(name, rows, cols, client);
		    model.GetmodelData().mutexGamePlaying.ReleaseMutex();
		    model.GetmodelData().mutexGameWating.ReleaseMutex();
            if (game == null)
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("game not exist", client);
            }
            return "multiPlayer";
        }

        /// <summary>
        /// Checks the valid.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public bool CheckValid(string[] args, TcpClient client)
        {
            if (args.Length > 3)
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
                return false;
            }
            try
            {
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
                return true;
            }
            catch (Exception)
            {
                Controller.NestedErrors nested = new Controller.NestedErrors("Bad arguement", client);
                return false;
            }
        }
    }
}
