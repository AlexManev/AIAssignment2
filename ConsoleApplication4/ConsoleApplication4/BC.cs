using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication4
{
    public class BC
    {
        private List<Data> _knowladgeBase;
        private List<Data> _queue;
        private List<string> _resultQueue;

        public BC(string fileName)
        {
            _knowladgeBase = new List<Data>();
            _queue = new List<Data>();
            _resultQueue = new  List<string>();
            ReadFromInput(fileName);
            Start();
        }

        /// <summary>
        /// Start Backwards Chaining.
        /// It searches he knowladgebase until 
        /// </summary>
        private void Start()
        {
            bool result = false;
            while (_queue[0].Implies != null)
            {
                foreach (Data data in _knowladgeBase)
                {
                    if (data.AreYou(_queue[0].ID))
                    {
                        result = true;
                        string[] implies = _queue[0].Implies.Split('&');
                        foreach (string imp in implies)
                        {
                            if (!_queue.Contains(FindInKB(imp)))
                            {
                                if (FindInKB(imp).Implies == null)
                                    _queue.Add(FindInKB(imp));
                                else
                                    _queue.Insert(1, FindInKB(imp));
                            }

                            if(!_resultQueue.Contains(imp))
                                _resultQueue.Insert(0,imp);
                        }

                        _queue.Remove(_queue[0]);
                        break;
                    }
                }
            }

            End(result);
        }
        /// <summary>
        /// Search through the knowladgebase for a data with id identical to the perimeter
        /// If the id is matching with one of the existing Data, return the existing data
        /// Otherwise new data is added to the knowladgebase and the new data object is rerurned
        /// </summary>
        private Data FindInKB(string id)
        {
            Data result = null;
            foreach (Data data in _knowladgeBase)
                if (data.AreYou(id))    //check if the data already exists in the KB
                    result = data;
            if (result == null)         //if it doesnt exist in the KB
            {
                result = new Data(id, null);
                _knowladgeBase.Add(result);
            }
            return result;
        }

        /// <summary>
        /// Print the result.
        /// </summary>
        private void End(bool result)
        {
            if (result == false)        //if no result found
            {
                Console.WriteLine("NO:");
            }
            else
            {
                Console.Write("YES:");
                for (int i = 0; i < _resultQueue.Count; i++)
                {
                    Console.Write(" {0}", _resultQueue[i]);

                    if (i != _resultQueue.Count-1)
                        Console.Write(",");
                }
            }
        }

        /// <summary>
        /// Reads form input file
        /// </summary>
        private void ReadFromInput(string fileName)
        {
            StreamReader _stream = new StreamReader(fileName);
            string data;

            data = _stream.ReadLine();
            while (data != null)
            {
                if (data == "TELL")
                {
                    data = _stream.ReadLine();
                    string[] values = data.Split(';');  //split the whole line on separater rules
                    foreach (string v in values)        
                    {
                        string[] rule = v.Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);  //split rules 
                        bool contains = false;

                        if (rule.Length == 1)
                        {
                            string e1 = ((rule[0].Trim())).Trim();
                            foreach (Data d in _knowladgeBase)
                                if (d.AreYou(e1))
                                    contains = true;
                            if (!contains)
                                _knowladgeBase.Add(new Data(e1, null));
                        }
                        else if (rule.Length == 2)
                        {
                            string query1 = ((rule[0].Trim())).Trim();    //trim white spaces
                            string query2 = ((rule[1].Trim())).Trim();    //trim white spaces
                            foreach (string elem in rule)
                            {
                                foreach (Data d in _knowladgeBase)
                                    if (d.AreYou(query2))
                                        contains = true;

                                if (!contains)
                                    _knowladgeBase.Add(new Data(query2, query1));
                            }
                        }
                    }

                }
                else if (data == "ASK")
                {
                    bool foundInKB = false;
                    data = _stream.ReadLine();
                    foreach (Data d in _knowladgeBase)
                    {
                        if (d.AreYou(data))
                        {
                            foundInKB = true;
                            _queue.Add(d);
                            _resultQueue.Add(d.ID);
                            break;
                        }
                    }
                    if (!foundInKB)
                    {
                        Console.WriteLine("Error: invalid quiry, please change the ASK quiry \n \nPress any key to continue...");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
                data = _stream.ReadLine();
            }
        }
    }
}
