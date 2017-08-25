using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class StarredRepos
    {
        public string Name { get; set;} // Repository name.
        public string Description { get; set; } // Displays the Description.
        public string Url { get; set; }
        public string Language { get; set; }
        public int Stargazers_Count { get; set; } // Displays the number of stars for this particular repo. Not currently working.
        public int Watchers { get; set; }
        public int Forks { get; set; }



        public static List<StarredRepos> GetStarredRepos()
        {
                RestClient client = new RestClient("https://api.github.com");
                RestRequest request = new RestRequest("/users/eluts15/starred", Method.GET);
                client.AddDefaultHeader("User-Agent", "eluts15"); // Github requires this header.
                RestResponse response = new RestResponse();
                Task.Run(async () =>
                {
                    response = await GetResponseContentAsync(client, request) as RestResponse;
                }).Wait();

                JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(response.Content);
                var starredRepositories = JsonConvert.DeserializeObject<List<StarredRepos>>(jsonResponse.ToString());
                return starredRepositories;
            }

            public static Task<IRestResponse> GetResponseContentAsync(RestClient client, RestRequest request)
            {
                var tcs = new TaskCompletionSource<IRestResponse>();
                client.ExecuteAsync(request, response => {
                    tcs.SetResult(response);
                });
                return tcs.Task;
            }
    }
}
