using System;

namespace AI_Assignment2
{
    class TruthSolver
    {
        public TruthSolver() { }

        public char Calculate(string input)
        {
            TruthTablePostFix TTPF = new TruthTablePostFix(input);
            return TTPF.Solve();
        }
    }
}