using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ModelDataBase
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, Solution<Position>> bfsSolutions;
        private Dictionary<string, Solution<Position>> dfsSolutions;
        private Dictionary<string, GameMultiPlayer> gameWating;
        private Dictionary<string, GameMultiPlayer> gamesPlaying;

        public ModelDataBase()
        {
            mazes = new Dictionary<string, Maze>();
            bfsSolutions = new Dictionary<string, Solution<Position>>();
            dfsSolutions = new Dictionary<string, Solution<Position>>();
            gameWating = new Dictionary<string, GameMultiPlayer>();
            gamesPlaying = new Dictionary<string, GameMultiPlayer>();
        }

        public Dictionary<string, Maze> Mazes
        {
            get { return mazes; }
        }
        public Dictionary<string, Solution<Position>> BfsSolutions
        {
            get { return bfsSolutions; }
        }
        public Dictionary<string, Solution<Position>> DfsSolutions
        {
            get { return dfsSolutions; }
        }
        public Dictionary<string, GameMultiPlayer> GameWating
        {
            get { return gameWating; }
        }
        public Dictionary<string, GameMultiPlayer> GamesPlaying
        {
            get { return gamesPlaying; }
        }


        public void AddMazes(string name , Maze maze)
        {
            mazes.Add(name, maze);
        }
        public void AddBfsSolutions(string name, Solution<Position> solution)
        {
            bfsSolutions.Add(name, solution);
        }
        public void AddDfsSolutions(string name, Solution<Position> solution)
        {
            dfsSolutions.Add(name, solution);
        }
        public void AddGame(string name, GameMultiPlayer game)
        {
            gameWating.Add(name, game);
        }
        public void DeleteGame(string name)
        {
            gameWating.Remove(name);
        }
        public void AddGamePlaying(string name, GameMultiPlayer game)
        {
            gamesPlaying.Add(name, game);
        }
        public void DeleteGamePlaying(string name)
        {
            gamesPlaying.Remove(name);
        }
    }
}
