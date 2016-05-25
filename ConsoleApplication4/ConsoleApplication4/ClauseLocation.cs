using System;

namespace AI_Assignment2
{
    class ClauseLocation
    {
        private string _clause;
        private int _location;

        public ClauseLocation(string clause, int location)
        {
            _clause = clause;
            _location = location;
        }

        public string GetClause
        {
            get { return _clause; }
        }

        public int GetLocation
        {
            get { return _location; }
        }
    }
}
