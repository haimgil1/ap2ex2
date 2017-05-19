using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Solution.
    /// </summary>
    public class Solution<T>
    {
        /// <summary>
        /// The stack.
        /// </summary>
        private Queue<State<T>> queue;
        /// <summary>
        /// The evaluated nodes.
        /// </summary>
        private int evaluatedNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SearchAlgorithmsLib.Solution`1"/> class.
        /// </summary>
        /// <param name="stack">Stack.</param>
        public Solution(Queue<State<T>> queue, int evaluatedNodes)
        {
            this.queue = queue;
            this.evaluatedNodes = evaluatedNodes;
        }

        /// <summary>
        /// Gets the stack.
        /// </summary>
        /// <value>The stack.</value>
        public Queue<State<T>> Queue
        {
            get { return queue; }
        }

        /// <summary>
        /// Gets the evaluated nodes.
        /// </summary>
        /// <value>The evaluated nodes.</value>
        public int EvaluatedNodes
        {
            get { return evaluatedNodes; }
        }

        /// <summary>
        /// Size this instance.
        /// </summary>
        /// <returns>The size.</returns>
        public int Size()
        {
            return queue.Count;
        }
    }
}
