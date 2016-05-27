using System;

namespace AI_Assignment2
{
    class ClauseLocation
    {
        private string _clause;
        private int _location;

        /// <summary>
        /// Contains a single clause and it's location
        /// </summary>
        /// <param name="clause"></param>
        /// <param name="location"></param>
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
