using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DadJokeConsole
{
    enum JokeGroups
    {
        Short = 0,
        Medium = 1,
        Long = 3
    }

    class RenderJokes
    {
        private int WordsCount(string str)
        {
            return Regex.Matches(str, @"(([\w'0-9]+(\s*)))").Count;
        }

        private bool JokeInGroup(string joke, JokeGroups group)
        {
            int count = WordsCount(joke);
            bool inGroup = false;

            if (group == JokeGroups.Short && count < 10)
            {
                inGroup = true;
            } else if (group == JokeGroups.Medium && count >= 10 && count < 20)
            {
                inGroup = true;
            } 
            else if (group == JokeGroups.Long && count >= 20) 
            {
                inGroup = true;
            }

            return inGroup;
        }

        private void PrintGroupLabel(JokeGroups group)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            switch (group)
            {
                case JokeGroups.Short:
                    Console.WriteLine("Short Jokes");
                    break;
                case JokeGroups.Medium:
                    Console.WriteLine("Medium Length Jokes");
                    break;
                case JokeGroups.Long:
                    Console.WriteLine("Long Jokes");
                    break;
            }
            Console.ResetColor();
        }

        public void PrintJoke(string joke, string searchTerm=null)
        {
            if (searchTerm == null || searchTerm.Length == 0)
            {
                Console.WriteLine(joke);
            }
            else
            {

                int positionOfTerm = joke.IndexOf(searchTerm);
                int startPos = 0;
                while (positionOfTerm >= 0) {
                    string str = joke.Substring(startPos, positionOfTerm - startPos);
                    // Write the leading bit of the string
                    Console.Write(str);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(searchTerm);
                    Console.ResetColor();
                    startPos = positionOfTerm + searchTerm.Length;
                    positionOfTerm = joke.IndexOf(searchTerm, startPos);
                }

                if (startPos < joke.Length) {
                    Console.WriteLine(joke.Substring(startPos));
                } else {
                    Console.WriteLine("");
                }
            }
        }

        private void PrintJokeGroup(List<Joke> jokes, string searchTerm, JokeGroups group)
        {
            PrintGroupLabel(group);

            foreach (var joke in jokes) {
                if (JokeInGroup(joke.joke, group))
                {
                    PrintJoke(joke.joke, searchTerm);
                }
            }
        }

        public void ShowJokes(JokeSearchResults jokes, string searchTerm)
        {
            PrintJokeGroup(jokes.results, searchTerm, JokeGroups.Short);
            PrintJokeGroup(jokes.results, searchTerm, JokeGroups.Medium);
            PrintJokeGroup(jokes.results, searchTerm, JokeGroups.Long);
        }
    }
}
