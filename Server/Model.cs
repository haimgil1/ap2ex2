using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json;
using ObjectAdapter;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class Model : IModel
    {
        private ModelDataBase modelData;
        private static Mutex mutex = new Mutex();

        //private static Mutex mutex = new Mutex();
        public Model()
        {
            modelData = new ModelDataBase();
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            IMazeGenerator newMaze = new DFSMazeGenerator();
            Maze maze = null;
            if (!modelData.Mazes.ContainsKey(name))
            {
                maze = newMaze.Generate(rows, cols);
                modelData.AddMazes(name, maze);
            }
            else
            {
                maze = modelData.Mazes[name];
            }

            return maze;
        }

        public Solution<Position> solveMazeBFS(string name)
        {
            Solution<Position> solution = null;
            if (modelData.Mazes.ContainsKey(name))
            {
                ISearchable<Position> mazeObjectAdapter = new MazeAdapter(modelData.Mazes[name]);
                ISearcher<Position> BFS = new Bfs<Position>();


                if (modelData.BfsSolutions.ContainsKey(name))
                {
                    solution = modelData.BfsSolutions[name];
                }
                else
                {
                    solution = BFS.Search(mazeObjectAdapter);
                    modelData.AddBfsSolutions(name, solution);
                }
            }
            return solution;

        }
        public Solution<Position> solveMazeDFS(string name)
        {
            Solution<Position> solution = null;
            if (modelData.Mazes.ContainsKey(name))
            {
                ISearchable<Position> mazeObjectAdapter = new MazeAdapter(modelData.Mazes[name]);
                ISearcher<Position> DFS = new Dfs<Position>();

                if (modelData.DfsSolutions.ContainsKey(name))
                {
                    solution = modelData.DfsSolutions[name];
                }
                else
                {
                    solution = DFS.Search(mazeObjectAdapter);
                    modelData.AddDfsSolutions(name, solution);
                }
            }
            return solution;
        }

        public GameMultiPlayer GenerateGame(string name, int rows, int cols, TcpClient client1)
        {
            IMazeGenerator newMaze = new DFSMazeGenerator();
            Maze maze;
            GameMultiPlayer game = null;
            if (!modelData.GameWating.ContainsKey(name) &&
                !modelData.GamesPlaying.ContainsKey(name))
            {
                maze = newMaze.Generate(rows, cols);
                maze.Name = name;
                game = new GameMultiPlayer(maze, client1);
                modelData.AddGame(name, game);
            }
            return game;

        }
        public void RemoveGameWating(string name)
        {
            modelData.DeleteGame(name);
        }
        public void RemoveGamePlaying(string name)
        {
            modelData.DeleteGamePlaying(name);
        }
        public void AddGamePlaying(string name, GameMultiPlayer game)
        {
            modelData.AddGamePlaying(name, game);
        }


        public bool ContainMaze(string name)
        {
            return modelData.Mazes.ContainsKey(name);
        }


        public string ListGames()
        {
            StringBuilder sb = new StringBuilder("[" + '\n');
            foreach (string key in modelData.GameWating.Keys)
            {
                sb.Append('"');
                sb.Append(key + '"' + ',' + '\n');
            }
            sb.Remove(sb.Length - 2, 1);
            sb.Append("]");
            string result = sb.ToString();
            return result;
        }
        public GameMultiPlayer FindGameWating(string name)
        {
            if (modelData.GameWating.ContainsKey(name))
            {
                return modelData.GameWating[name];
            }
            return null;
        }
        public GameMultiPlayer FindGamePlaying(string name)
        {
            if (modelData.GamesPlaying.ContainsKey(name))
            {
                return modelData.GamesPlaying[name];
            }
            return null;
        }

        public GameMultiPlayer FindGameByClient(TcpClient client)
        {
            GameMultiPlayer game = null;
            foreach (GameMultiPlayer value in modelData.GamesPlaying.Values)
            {
                if (value.GetClient1() == client || value.GetClient2() == client)
                {
                    game = value;
                }
            }
            return game;
        }
    }
}