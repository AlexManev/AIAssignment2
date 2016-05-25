using System;
using System.Collections.Generic;

namespace AI_Assignment2
{
    class TruthTable
    {
        private List<List<char>> _truthTable;
        private TableMap _tableMap; 
        private string[] _clauses;
        private string _goal;

        public TruthTable(string input)
        {
            ReadInput(input);
            Populate();
            CalculateTELL();
            CalculateASK();
        }

        public void ReadInput(string input)
        {
            _truthTable = new List<List<char>>();
            _tableMap = new TableMap();

            string[] lines = System.IO.File.ReadAllLines(@input);

            lines[1] = lines[1].Replace(" ", "");
            _clauses = lines[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            _goal = lines[3];
        }

        public void Populate()
        {
            List<string[]> truthValues =  new List<string[]>();
            List<string> uniqueTruthValues = new List<string>();

            foreach (string s in _clauses)
            {
                truthValues.Add(s.Split(new string[] { "&", "=>" }, StringSplitOptions.RemoveEmptyEntries));
            }

            foreach (string[] sa in truthValues)
            {
                foreach (string s in sa)
                {
                    if (!uniqueTruthValues.Contains(s))
                        uniqueTruthValues.Add(s);
                }
            }

            for (int i = 0; i < uniqueTruthValues.Count; i++)
            {
                _truthTable.Add(new List<char>());
                _tableMap.Add(uniqueTruthValues[i], i);
            }

            int totalValues = (int)Math.Pow(2, _truthTable.Count);

            for (int i = 0; i < _truthTable.Count; i++)
            {
                for (int j = 0; j < totalValues; j++)
                {
                    if ((int)(j / Math.Pow(2, i) % 2) == 0)
                        _truthTable[i].Add('F');
                    else
                        _truthTable[i].Add('T');
                }
            }
        }

        public void CalculateTELL()
        {
            string tempString = "";
            List<string> uniqueValues = new List<string>();

            for ( int i = 0; i < _clauses.Length; i++)
            {
                if (_clauses[i].Contains("&") || _clauses[i].Contains("=>"))
                {
                    foreach (char c in _clauses[i])
                    {
                        if (c != '=')
                        {
                            tempString = tempString + c;
                        }
                    }

                    uniqueValues.Add(tempString);

                    _clauses[i] = tempString;
                    tempString = "";
                }
            }

            TruthSolver truthSolver = new TruthSolver();
            string truthValue = "";

            foreach (string s in uniqueValues)
            {
                _truthTable.Add(new List<char>());
                _tableMap.Add(s, _truthTable.Count - 1);

                for (int i = 0; i < _truthTable[0].Count; i++)
                {
                    foreach (char c in s)
                    {
                        if (!TruthTablePostFix.IsOperator(c))
                        {
                            truthValue = truthValue + c;
                        }
                        else
                        {
                            truthValue = Convert.ToString(_truthTable[_tableMap.Find(Convert.ToString(truthValue))][i]);
                            tempString = tempString + truthValue + c;
                            truthValue = "";
                        }
                    }

                    tempString = tempString + Convert.ToString(_truthTable[_tableMap.Find(Convert.ToString(truthValue))][i]);

                    _truthTable[_tableMap.Find(s)].Add(truthSolver.Calculate(tempString));
                    tempString = "";
                    truthValue = "";

                }
            }
        }

        public void CalculateASK()
        {
            List<int> trueLocations = new List<int>();
            bool hasFalse = false;

            for (int i = 0; i < _truthTable[0].Count; i++)
            {
                for (int j = 0; j < _clauses.Length; j++)
                {
                    if (_truthTable[_tableMap.Find(_clauses[j])][i] == 'F')
                    {
                        hasFalse = true;
                        break;
                    }
                }

                if (!hasFalse)
                    trueLocations.Add(i);

                hasFalse = false;
            }

            int count = 0;

            for (int i = 0; i < trueLocations.Count; i++)
            {
                if (_truthTable[_tableMap.Find(_goal)][trueLocations[i]] == 'T')
                    count++;
            }

            if (count != 0)
                Console.WriteLine("YES: " + count);
            else
                Console.WriteLine("NO");
        }
    }
}
