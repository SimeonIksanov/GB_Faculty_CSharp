using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Test
{
    public class Grabber
    {
        readonly HttpClient _httpClient;

        public Grabber()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Post>> DownloadAsync(int[] ids)
        {
            Task<HttpResponseMessage>[] tasks = new Task<HttpResponseMessage>[ids.Length];

            for (int i = 0; i < ids.Length; i++)
            {
                tasks[i] = _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts/{ids[i]}");
            }

            var results = await Task.WhenAll(tasks);

            return await Task.WhenAll(
                results.Select(
                    async hrm => await hrm.Content
                                          .ReadFromJsonAsync<Post>()
                ));
        }
    }
}
