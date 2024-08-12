namespace BlazorCyber.Models
{
    public class AuthenticateRequestAndResponse
    {
        public bool IsSuccess { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public object RefreshToken { get; internal set; }

        public class AuthenticateRequestAndResponse : IAuthenticateRequestAndResponse
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public bool IsSuccess { get; set; }
            public string Content { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
