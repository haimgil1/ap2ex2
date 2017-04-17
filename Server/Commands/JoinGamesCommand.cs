using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    public class JoinGamesCommand : ICommand
    {
        private IModel model;
        public JoinGamesCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            // the name of the game to join.
            string name = args[0];
            GameMultiPlayer game = model.FindGameWating(name);
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);

            // check if the game is in the list of games to play.
            if (game != null)
            {
                game.Join(client);
                model.AddGamePlaying(name, game);
                model.RemoveGameWating(name);
                
                return "multiPlayer";
            }
            else
            {
                writer.WriteLine("Error exist game");
                writer.Flush();
                return "singlePlayer";
            }
        }
    }
}
