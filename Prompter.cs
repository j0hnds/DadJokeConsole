using System;
using System.Text.RegularExpressions;

namespace DadJokeConsole
{
    public class Prompter
    {

        Regex searchRe = new Regex(@"^s", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        Regex randomRe = new Regex(@"^r", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public bool SearchForJokes { get; set; }
        public string SearchTerm { get; set; }
        public bool GetRandomJokes { get; set; }

        public Prompter() 
        {
            searchRe = new Regex(@"^s", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            randomRe = new Regex(@"^r", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            SearchForJokes = false;
            SearchTerm = null;
            GetRandomJokes = false;
        }

        public void PromptUser()
        {
            bool gotAnswer = false;
            do {
                Console.WriteLine("");
                Console.WriteLine("Search (s) or Show Random Jokes (r)?");
                string response = Console.ReadLine();
                if (response == null)
                {
                    Console.WriteLine($"Need to supply an answer. Try again");
                }
                else if (searchRe.IsMatch(response))
                {
                    SearchForJokes = true;
                    Console.WriteLine("Enter search keyword; blank for all jokes");
                    response = Console.ReadLine();
                    SearchTerm = response;
                    gotAnswer = true;
                }
                else if (randomRe.IsMatch(response))
                {
                    GetRandomJokes = true;
                    gotAnswer = true;
                }
                else
                {
                    Console.WriteLine($"Invalid response: '{response}'. Try again");
                }
            } while (! gotAnswer);
        }
    }
}
