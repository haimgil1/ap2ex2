using MazeLib;
using SearchAlgorithmsLib;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Interface : Imodel.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns>Maze.</returns>
        Maze GenerateMaze(string name, int rows, int cols);

        /// <summary>
        /// Solves the maze BFS.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Solution&lt;Position&gt;.</returns>
        Solution<Position> solveMazeBFS(string name);

        /// <summary>
        /// Solves the maze DFS.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Solution&lt;Position&gt;.</returns>
        Solution<Position> solveMazeDFS(string name);

        /// <summary>
        /// Generates the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="client1">The client1.</param>
        /// <returns>GameMultiPlayer.</returns>
        GameMultiPlayer GenerateGame(string name, int rows, int cols, TcpClient client1);

        /// <summary>
        /// Contains the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ContainMaze(string name);

        /// <summary>
        /// Removes the game wating.
        /// </summary>
        /// <param name="name">The name.</param>
        void RemoveGameWating(string name);

        /// <summary>
        /// Removes the game playing.
        /// </summary>
        /// <param name="name">The name.</param>
        void RemoveGamePlaying(string name);

        /// <summary>
        /// Adds the game playing.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="game">The game.</param>
        void AddGamePlaying(string name, GameMultiPlayer game);

        /// <summary>
        /// Getmodels the data.
        /// </summary>
        /// <returns>ModelDataBase.</returns>
        ModelDataBase GetmodelData();

        /// <summary>
        /// Lists the games wating.
        /// </summary>
        /// <returns>System.String.</returns>
        string ListGamesWating();

        /// <summary>
        /// Finds the game wating.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>GameMultiPlayer.</returns>
        GameMultiPlayer FindGameWating(string name);

        /// <summary>
        /// Finds the game playing.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>GameMultiPlayer.</returns>
        GameMultiPlayer FindGamePlaying(string name);

        /// <summary>
        /// Finds the game by client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>GameMultiPlayer.</returns>
        GameMultiPlayer FindGameByClient(TcpClient client);

        /// <summary>
        /// Clients the on game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ClientOnGame(TcpClient client);

        /// <summary>
        /// Clients the name of the on game by.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ClientOnGameByName(TcpClient client, string name);

    }
}