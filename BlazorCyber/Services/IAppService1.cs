using BlazorCyber.Models;

namespace BlazorCyber.Services
{
    public interface IAppService1
    {
        Task<AuthenticateRequestAndResponse> AuthenticateUser(LoginModel loginModel);
        Task<bool> RefreshToken();
        Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registerUser);
    }
}