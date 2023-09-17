namespace GetFreelancer_API.Interface
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}