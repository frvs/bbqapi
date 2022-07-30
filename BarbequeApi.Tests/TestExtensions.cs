using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BarbequeApi.Tests
{
    public static class TestExtensions
    {
        public async static Task<(HttpStatusCode statusCode, T body, string? errorMessage)> GetAsync<T>(this HttpClient httpClient, string path)
        {
            var httpResponse = await httpClient.GetAsync(path);
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (string.IsNullOrEmpty(responseAsString))
                {
                    responseAsString = string.Empty;
                }

                return (httpResponse.StatusCode, (T)default, responseAsString);
            }
            var obj = JToken.Parse(responseAsString).ToObject<T>();

            return (httpResponse.StatusCode, obj, null);
        }

        public async static Task<(HttpStatusCode statusCode, string? errorMessage)> PostAsync<T>(this HttpClient httpClient, string path, T body)
        {
            var bodySerialized = JsonSerializer.Serialize(body);

            var httpResponse = await httpClient.PostAsync(path, new StringContent(bodySerialized, Encoding.UTF8, "application/json"));
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (string.IsNullOrEmpty(responseAsString))
                {
                    responseAsString = string.Empty;
                }

                return (httpResponse.StatusCode, responseAsString);
            }

            return (httpResponse.StatusCode, null);
        }

        public async static Task<HttpStatusCode> Delete(this HttpClient httpClient, string path) // its impossible to do a extension method named deleteasync (???)
        {
            var httpResponse = await httpClient.DeleteAsync(path);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return httpResponse.StatusCode;
            }

            return httpResponse.StatusCode;
        }
    }
}
