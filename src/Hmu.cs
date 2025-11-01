using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace HmuApi
{
    public class Hmu
    {
        private string jwtToken;
        private readonly HttpClient httpClient;
        private readonly string apiUrl = "https://flavo.herokuapp.com/api";
        public Hmu()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("okhttp/4.9.1");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        }

        public async Task<string> Login(string email, string password)
        {
            var data = JsonContent.Create(new
            {
                email = email,
                password = password
            });
            var response = await httpClient.PostAsync($"{apiUrl}/user/login", data);
            var content = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(content);
            if (document.RootElement.TryGetProperty("token", out var tokenElement))
            {
                jwtToken = tokenElement.GetString();
                httpClient.DefaultRequestHeaders.Add("Authorization", jwtToken);
            }
            return content;
        }

        public async Task<string> Register(string email, string password)
        {
            var data = JsonContent.Create(new
            {
                email = email,
                password = password
            });
            var response = await httpClient.PostAsync($"{apiUrl}/user/signup", data);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetChats(int skip = 0, int limit = 48)
        {
            var response = await httpClient.GetAsync($"{apiUrl}/chat_user?skip={skip}&limit={limit}");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAccountInfo()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/profile?profileId=me");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
