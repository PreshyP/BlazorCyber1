using BlazorCyber.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCyber.Services
{
    public interface IAppService
    {
        public Task<string> AuthenticateUser(LoginModel loginModel);
       // Task<ApiResponse<AuthenticateRequestAndResponse>> AuthenticateUser(LoginModel loginModel);
        Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registerUser);
    }
}

