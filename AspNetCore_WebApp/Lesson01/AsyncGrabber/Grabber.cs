using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Test
{
    public class Grabber : IDisposable
    {
        readonly HttpClient _httpClient;

        private bool disposedValue;

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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _httpClient.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Grabber()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
