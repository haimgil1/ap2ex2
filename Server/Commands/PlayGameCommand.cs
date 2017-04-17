using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class PlayGameCommand : ICommand
    {
        private IModel model;
        public PlayGameCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client)
        {
            string direction = args[0];
            GameMultiPlayer game = model.FindGameByClient(client);
            NestedPlay nested = new NestedPlay(game.GetMaze().Name, direction);
            NetworkStream stream = null;

            stream = game.OtherClient(client).GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(JsonConvert.SerializeObject(nested));
            writer.Flush();
            return "multiPlayer";
        }

        public class NestedPlay
        {
            public string Name;
            public string Direction;

            public NestedPlay(string name1, string direction1)
            {
                this.Name = name1;
                this.Direction = direction1;
            }
        }
    }
}
