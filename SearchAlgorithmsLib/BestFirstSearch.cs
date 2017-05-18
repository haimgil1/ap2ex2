using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Bfs.
    /// </summary>
    public class BestFirstSearch<T> : QueueSearcher<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SearchAlgorithmsLib.BestFirstSearch`1"/> class.
        /// </summary>
        public BestFirstSearch() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SearchAlgorithmsLib.BestFirstSearch`1"/> class.
        /// </summary>
        /// <param name="comparer">Comparer.</param>
        public BestFirstSearch(Comparer<State<T>> comparer) : base(comparer)
        {

        }

        /// <summary>
        /// Search the specified searchable.
        /// </summary>
        /// <returns>The solution.</returns>
        /// <param name="searchable">Searchable.</param>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            AddToOpenList(searchable.GetInitialState()); // inherited from Searcher
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> n = PopOpenList(); // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(searchable.GetIGoallState()))
                {
                    return BackTrace(n, searchable.GetInitialState()); // private method, back traces through the parents
                                                                       // calling the delegated method, returns a list of states with n as a parent	
                }

                List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !openList.Contains(s))
                    {
                        s.CameFrom = n;
                        AddToOpenList(s);
                    }
                    else
                    {
                        if (searchable.isBetterWay(s,n))
                        {
                            s.CameFrom = n;
                            searchable.updateCost(s, n);

                            if (!openList.Contains(s))
                            {
                                AddToOpenList(s);
                            }
                            else
                            {
                                openList.RemoveItem(s);
                                openList.Enqueue(s);                 
                            }
                        }
                    }
                }
            }
            return null;
        }
    }


}