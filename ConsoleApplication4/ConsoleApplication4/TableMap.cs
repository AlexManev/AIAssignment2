using System;
using System.Collections.Generic;

namespace AI_Assignment2
{
    class TableMap
    {
        private List<ClauseLocation> _clauseList;

        /// <summary>
        /// Contains a list of clauses and their locations.
        /// </summary>
        public TableMap()
        {
            _clauseList = new List<ClauseLocation>();
        }

        public void Add(string clause, int location)
        {
            _clauseList.Add(new ClauseLocation(clause, location));
        }

        public int Find(string input)
        {
            foreach (ClauseLocation c in _clauseList)
            {
                if (input == c.GetClause)
                {
                    return c.GetLocation;
                }
            }

            throw new IndexOutOfRangeException("Could not find clause in table map");
        }

        public List<int> Contains(string input)
        {
            List<int> containedLocations = new List<int>();

            foreach (ClauseLocation cl in _clauseList)
            {
                if (cl.GetClause.Contains(input))
                    containedLocations.Add(cl.GetLocation);
            }

            return containedLocations;
        }
    }
}
