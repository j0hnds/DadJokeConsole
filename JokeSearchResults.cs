using System.Collections.Generic;

namespace DadJokeConsole
{

    public class JokeSearchResults
    {
        public int current_page { get; set; }
        public int limit { get; set; }
        public int next_page { get; set; }
        public int previous_page { get; set; }
        public List<Joke> results { get; set; }
    }

}
