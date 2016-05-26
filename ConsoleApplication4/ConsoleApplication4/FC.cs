using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment2
{
    //Forward Chaining
    public class FC
    {
        public static string TELL;
        public static string ASK;
        string filename;
        public static List<string> agenda;
        public static List<Data> clauses;
        public static List<string> entailed;

        public FC(string _filename)
        {
            filename = _filename;
            agenda = new List<string>();
            clauses = new List<Data>();
            entailed = new List<string>();
            var list = new List<string>();

            //Get ASK & TELL from file
            string[] lines = File.ReadAllLines(filename);
            for (int i = 0; i < lines.Count(); i++)
            {
                if (lines[i].Contains("TELL"))
                {
                    TELL = lines[i + 1];
                }
                else if (lines[i].Contains("ASK"))
                {
                    ASK = lines[i + 1];
                }
            }
            //Get clauses and agenda
            initialise();
            //Compute result
            fcalgorithm();
            //Print result
            print();

        }

    public void print()
        {
            //if the fc algorythm returns true, print all of the items in entailed
            if(fcalgorithm())
            {
                Console.Write("YES:");
                foreach(string s in entailed)
                {
                    Console.Write(s+", ");
                }
            }
            else
            {
                Console.Write("NO:");
            }
        }

    public void initialise()
        {
            //convert everything to lower case and remove whitespaces
            ASK = ASK.ToLower();
            TELL = TELL.Replace(" ", string.Empty);
            TELL = TELL.ToLower();

            //break up the tell string by the semicolens
            string[] sentences = TELL.Split(new string[] { ";" },StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sentences.Length; i++)
            {
                //get the elements for the agenda
                if (!sentences[i].Contains("=>"))
                {
                    agenda.Add(sentences[i].ToLower());
                }
                else
                {
                    //get all the clauses
                    string[] implies = sentences[i].Split(new string[] { "=>" },StringSplitOptions.None);
                    Data clause = new Data(implies[0], implies[1]);
                    clauses.Add(clause);
               

                }
            }
        }
    //start the forward chaning algorythm
    public bool fcalgorithm()
        {
            //keep trying until we find a solution or there is nothing left in the agenda
            while (agenda.Count != 0)
            {
                //get the first thing on the agenda
                string check = agenda.First();
                //add the checked item to the agenda if it hasnt already been added
                if (!entailed.Contains(check))
                {
                    entailed.Add(check);
                }

                //Check if the agenda contains the item we're looking for
                if (agenda.Contains(ASK))
                {
                    if (!entailed.Contains(ASK))
                    {
                        entailed.Add(ASK);
                    }

                    return true;
                }
                else
                {
                    //check each clause
                    foreach (Data t in clauses)
                    {
                        //if its a combo clause see if both items are in the agenda
                        if (t.ID.Contains("&"))
                        {
                            string[] and = t.ID.Split('&');
                            int count = 0;
                            for (int i = 0; i < and.Length; i++)
                            {
                                if (agenda.Contains(and[i]))
                                {
                                    count++;
                                }
                            }
                            //if all of the items in the clause are in the agenda add
                            //the found item to the agenda
                            if (count == and.Length)
                            {
                                agenda.Add(t.Implies);
                                for (int i = 0; i < and.Length; i++)
                                {
                                    //add all agenda items to entailed if they're not already in there
                                    if (!entailed.Contains(and[i]))
                                    {
                                        entailed.Add(and[i]);
                                    }
                                }

                            }
                        }
                        //check if the agenda item is in the clause
                        if (t.AreYou(check))
                        {
                            agenda.Add(t.Implies);
                            //check if what we just added to the agenda is what we're looking for
                            if (agenda.Contains(ASK))
                            {
                                if (!entailed.Contains(ASK))
                                {
                                    entailed.Add(ASK);
                                }

                                return true;
                            }
                        }
                    }
                }
                //remove the checked item from the agenda
                agenda.Remove(check);
            }
            //couldnt find the item we wanted
            return false;
        }
    }
}
