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
                RestRequest request = new RestRequest("search/repositories?q=user:eluts15&sort=stars&order=desc&per_page=3", Method.GET); //I broke it hehe.
                client.AddDefaultHeader("User-Agent", "eluts15"); // Github requires this header.
                RestResponse response = new RestResponse();
                Task.Run(async () =>
                {
                    response = await GetResponseContentAsync(client, request) as RestResponse;
                }).Wait();

                JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
                var starredRepositories = JsonConvert.DeserializeObject<List<StarredRepos>>(jsonResponse["items"].ToString());
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
