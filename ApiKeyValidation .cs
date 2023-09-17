using GetFreelancer_API.Interface;

namespace GetFreelancer_API
{
    public class ApiKeyValidation : IApiKeyValidation
    {
        private readonly IConfiguration _configuration;

        public ApiKeyValidation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValidApiKey(string userApiKey)
        {
            if (string.IsNullOrWhiteSpace(userApiKey))
                return false;

            string? apiKey = Environment.GetEnvironmentVariable("API_KEY");

            if (apiKey == null || apiKey != userApiKey)
                return false;

            return true;
        }
    }
}
