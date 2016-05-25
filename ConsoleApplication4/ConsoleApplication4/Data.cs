using System;

namespace AI_Assignment2
{
    public class Data
    {
        private string _implies;
        private string _id;

        public Data(string id, string implies)
        {
            _implies = implies.ToLower();
            _id = id.ToLower();
        }

        public bool AreYou(string id)
        {
            id = id.ToLower();
            return id == _id;
        }

        public string ID
        {
            get { return _id; }
        }
        public string Implies
        {
            get { return _implies; }
        }
    }
}
