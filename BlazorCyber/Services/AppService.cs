using BlazorCyber.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCyber.Services
{
    public class AppService : IAppService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:3000";

        public AppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AuthenticateUser(LoginModel loginModel)
        {
            var url = $"{_baseUrl}{APIs.AuthenticateUser}";
            var serializedStr = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(serializedStr, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                // Log error or throw exception based on your error handling strategy
                throw new HttpRequestException($"Authentication failed with status code {response.StatusCode}");
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registerUser)
        {
            var url = $"{_baseUrl}{APIs.RegisterUser}";
            var serializedStr = JsonConvert.SerializeObject(registerUser);
            var content = new StringContent(serializedStr, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, errorMessage);
            }
        }
    }
}
