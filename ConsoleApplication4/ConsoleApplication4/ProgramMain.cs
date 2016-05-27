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
            switch (args[0].ToLower())
            {
                case "fc":
                	FC fc = new FC(args[1]);
                    break;
                case "bc":
                    BC bc = new BC(args[1]);
                    break;
                case "tt":
                    TruthTable table = new TruthTable(args[1]);
                    break;
            }
        }
    }
}
