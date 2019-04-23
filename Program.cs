using System;
using System.Threading.Tasks;
using System.Timers;

namespace DadJokeConsole
{
    class Program
    {
        private static ICanHazDadJokeAPI iCHDJApi = new ICanHazDadJokeAPI();
        private static Timer theTimer;
        private static RenderJokes renderJokes = new RenderJokes();

        static void Main(string[] args)
        {
            Prompter prompt = new Prompter();
            prompt.PromptUser();
            if (prompt.SearchForJokes)
            {
                Console.Clear();
                SearchForJokesAsync(prompt.SearchTerm)
                    .GetAwaiter()
                    .GetResult();
            }
            else if (prompt.GetRandomJokes)
            {
                Console.Clear();
                Console.WriteLine("A new joke will be told every 10 seconds; hit <enter> to stop.");
                Console.WriteLine();
                GetRandomJokeAsync()
                    .GetAwaiter();
                SetTimer();
                Console.ReadLine();
                theTimer.Stop();
                theTimer.Dispose();
                Console.WriteLine("Thanks for playing.");
            }
            else
            {
                Console.WriteLine("Unknown command. Exiting");
            }
        }

        private static async Task GetRandomJokeAsync()
        {
            try
            {
                Joke joke = await iCHDJApi.GetRandomJokeAsync();
                renderJokes.PrintJoke(joke.joke);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static async Task SearchForJokesAsync(string searchTerm)
        {
            try
            {
                JokeSearchResults jokes = await iCHDJApi.SearchForJokesAsync(searchTerm);
                renderJokes.ShowJokes(jokes, searchTerm);
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void SetTimer()
        {
            theTimer = new Timer(10000);

            // Hookup the callback
            theTimer.Elapsed += OnTimedEvent;
            theTimer.AutoReset = true;
            theTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine();

            GetRandomJokeAsync()
                .GetAwaiter();
        }
    }
}
