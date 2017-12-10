using System;
using System.Collections;
using System.IO;

namespace ccslabsConsoleAskQuestionGetAnswer
{
    class Program
    {
        private static readonly ArrayList AllResults = new ArrayList(); // Global Array

        static void Main(string[] args)
        {
            string Response = "y";

            while (Response.ToLower() == "y")
            {
                Ask("Enter Student First Name : ");
                Ask("Enter Student Second Name : ");
                Ask("Enter Age: ");
                Ask("Enter gender : ");
                Response = Ask("Do you want to enter more results? Y/n", true); // Defaults to Y
            }

            // Ok save the results
            SaveResults();
            WaitKey();
            ShowResults();
            WaitKey();

        }

        private static string Ask(string s, bool YesNo = false)
        {
            if (YesNo == true) // We have asked if they want to do more questions
            {
                Console.Write(s);
                string res = Console.ReadLine();
                if (res.ToLower() == "y" || res.Length < 1)
                {
                    // They said yes - or nothing - defaults to Yes
                    Console.Clear(); // Clear the screen for the next set of questions
                    return "y";
                }
                else
                {
                    return "n"; // No more enteries
                }
            }
            else
            {
                // Ok we are asking a normal Question
                Console.Write(s);
                AllResults.Add(Console.ReadLine());
                //   Console.Write(Environment.NewLine); // Move to next line for next question
                return ""; // no need to return a value for this
            }
        }

        private static void SaveResults()
        {

            FileStream fs = new FileStream(@"stdinfo.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Inheritable);
            StreamWriter sw = new StreamWriter(fs);

            foreach (string val in AllResults)
            {
                sw.WriteLine(val);
            }

            sw.Close();
            fs.Close();
        }

        private static void ShowResults()
        {
            int Question = 0;
            string result = "";
            Console.Clear(); // Clears the screen
            FileStream fs = new FileStream("stdinfo.txt", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Inheritable);
            StreamReader sr = new StreamReader(fs);

            // Get results for the 4 questions and show them
            while (!sr.EndOfStream)
            {
                if (Question < 4)
                {
                    result += sr.ReadLine() + "|";
                    Question++;
                }
                else
                {
                    Question = 0;
                    Console.WriteLine(result.Trim('|'));
                    result = ""; // result the results
                }
            }

            sr.Close();
            fs.Close();
        }

        private static void WaitKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(false);
        }
    }
}
