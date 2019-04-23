using System;
using System.Text.RegularExpressions;

namespace DadJokeConsole
{
    public class Prompter
    {

        private Regex searchRe = new Regex(@"^s", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Regex randomRe = new Regex(@"^r", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Regex validSearchRE = new Regex(@"^[\w'0-9]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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

        private void ProcessSearchTerm(string searchTerm)
        {
            SearchTerm = searchTerm;

            if (searchTerm == null)
            {
                SearchTerm = "";
            }
            else {
                searchTerm = searchTerm.Trim();

                if (searchTerm != null && searchTerm.Length > 0)
                {
                    // Make sure this is a 'valid' search term. I've decided that 'valid' means
                    // that the search term is a single word that may contain alphanumeric characters
                    // and an apostrophe.
                    if (! validSearchRE.IsMatch(searchTerm))
                    {
                        // For now, if the search term isn't 'valid', I'll just mash it 
                        // down to an empty string. This situation should probably be
                        // handled in the UI, but - at a minimum - I want to not pass a
                        // bad search term along to the API.
                        SearchTerm = "";
                    }
                }
            }
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
                    ProcessSearchTerm(response);
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
