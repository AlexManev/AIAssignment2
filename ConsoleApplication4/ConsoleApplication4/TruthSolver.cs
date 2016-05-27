using System;

namespace AI_Assignment2
{
    class TruthSolver
    {
        /// <summary>
        /// Creates a TruthTablesPostFix object, solves it and returns the values.
        /// </summary>
        public TruthSolver() { }

        public char Calculate(string input)
        {
            TruthTablePostFix TTPF = new TruthTablePostFix(input);
            return TTPF.Solve();
        }
    }
}