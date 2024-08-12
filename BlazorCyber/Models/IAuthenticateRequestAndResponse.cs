namespace BlazorCyber.Models
{
    public interface IAuthenticateRequestAndResponse
    {
        string AccessToken { get; set; }
        string Content { get; set; }
        string ErrorMessage { get; set; }
        bool IsSuccess { get; set; }
        string RefreshToken { get; set; }
    }
}