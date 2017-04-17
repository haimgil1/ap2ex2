using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ListGamesCommand : ICommand
    {
        private IModel model;
        public ListGamesCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(model.ListGames());
            writer.Flush();

            return "multiPlayer";
        }
    }
}
