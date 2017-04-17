using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class CloseGameCommand : ICommand
    {
        private IModel model;
        public CloseGameCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            GameMultiPlayer game = model.FindGamePlaying(name);
            // check if the game is in the list of games to play.
            if (game != null)
            {
                model.RemoveGamePlaying(name);
                writer.WriteLine("close client do close");
                writer.Flush();

                stream = game.OtherClient(client).GetStream();
                writer = new StreamWriter(stream);
                writer.WriteLine("close");
                writer.Flush();
                return "singlePlayer";
            }
            else
            {
                writer.WriteLine("Error exist game");
                return "multiPlayer";
            }
 
        }
    }
}
