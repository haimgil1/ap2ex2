using MazeLib;
using SearchAlgorithmsLib;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    public interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);
        Solution<Position> solveMazeBFS(string name);
        Solution<Position> solveMazeDFS(string name);
        GameMultiPlayer GenerateGame(string name, int rows, int cols, TcpClient client1);

        void RemoveGameWating(string name);
        bool ContainMaze(string name);
        void RemoveGamePlaying(string name);
        void AddGamePlaying(string name, GameMultiPlayer game);
      
        string ListGames();
        GameMultiPlayer FindGameWating(string name);
        GameMultiPlayer FindGamePlaying(string name);
        GameMultiPlayer FindGameByClient(TcpClient client);

        //Dictionary<string, GameMultiPlayer> GetGames();
        //Dictionary<string, Maze> GetMazes();
        //bool ContainGamePlaying(string name);
        //bool ContainGame(string name);
    }
}