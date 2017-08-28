using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class StarredRepos
    {
        public string Name { get; set;}
        public string Description { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public int Stargazers_Count { get; set; }
        public int Watchers_Count { get; set; }

        public static List<StarredRepos> GetStarredRepos()
        {
                RestClient client = new RestClient("https://api.github.com");
                RestRequest request = new RestRequest("search/repositories?q=user:eluts15+fork:true&sort=stars&order=desc&page=1&page&per_page=3", Method.GET);
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
