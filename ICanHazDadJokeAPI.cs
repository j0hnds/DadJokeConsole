using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DadJokeConsole
{

    public class ICanHazDadJokeAPI
    {
        private Uri baseEndpoint;
        private HttpClient client;

        public ICanHazDadJokeAPI()
        {
            baseEndpoint = new Uri("https://icanhazdadjoke.com/");

            SetupClient();
        }

        private void SetupClient()
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            // We want only application JSON responses
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            // Set the User agent to be kind to the API
            ProductHeaderValue header = new ProductHeaderValue("DadJokeConsole", 
                    "j0hnds-DadJokeConsole");
            ProductInfoHeaderValue userAgent = new ProductInfoHeaderValue(header);
            client.DefaultRequestHeaders.UserAgent.Add(userAgent);

            client.BaseAddress = baseEndpoint;
        }

        public async Task<Joke> GetRandomJokeAsync()
        {
            Joke joke = null;

            HttpResponseMessage response = await client.GetAsync(baseEndpoint.PathAndQuery);
            if (response.IsSuccessStatusCode)
            {
                joke = await response.Content.ReadAsAsync<Joke>();
            }

            return joke;
        }

        public async Task<JokeSearchResults> SearchForJokesAsync(string searchTerm, int page=1, int limit=30)
        {
            JokeSearchResults jokes = null;

            string searchUri = $"search?page={page}&limit={limit}";
            if (searchTerm != null && searchTerm.Length > 0)
            {
                searchUri += $"&term={searchTerm}";
            }

            Uri fullSearchUri = new Uri(baseEndpoint, searchUri);

            HttpResponseMessage response = await client.GetAsync(fullSearchUri.PathAndQuery);
            if (response.IsSuccessStatusCode)
            {
                jokes = await response.Content.ReadAsAsync<JokeSearchResults>();
            }

            return jokes;
        }
    }
}
