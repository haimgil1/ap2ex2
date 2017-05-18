using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Searchable.
    /// </summary>
    public interface ISearchable<T>
    {
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>State&lt;T&gt;.</returns>
        State<T> GetInitialState();

        /// <summary>
        /// Gets the state of the i goall.
        /// </summary>
        /// <returns>State&lt;T&gt;.</returns>
        State<T> GetIGoallState();

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>List&lt;State&lt;T&gt;&gt;.</returns>
        List<State<T>> GetAllPossibleStates(State<T> s);

        /// <summary>
        /// Updates the cost.
        /// </summary>
        /// <param name="cameFrom">The came from.</param>
        /// <param name="current">The current.</param>
        void updateCost(State<T> cameFrom, State<T> current);

        /// <summary>
        /// Determines whether [is better way] [the specified oldpath].
        /// </summary>
        /// <param name="oldpath">The oldpath.</param>
        /// <param name="newpath">The newpath.</param>
        /// <returns><c>true</c> if [is better way] [the specified oldpath]; otherwise, <c>false</c>.</returns>
        bool isBetterWay(State<T> oldpath, State<T> newpath);
    }
}
