using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            //TruthTable _table = new TruthTable("C:/Users/Jake/Desktop/test1.txt");

            switch (args[0].ToLower())
            {
                case "fc":
                    //fc
                    break;
                case "bc":
                    //bc
                    break;
                case "tt":
                    TruthTable _table = new TruthTable(args[1]);
                    break;
            }
        }
    }
}
