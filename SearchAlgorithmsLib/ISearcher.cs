using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Searcher.
    /// </summary>
    public interface ISearcher<T>
    {      
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>Solution&lt;T&gt;.</returns>
        Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int GetNumberOfNodesEvaluated();
    }
}