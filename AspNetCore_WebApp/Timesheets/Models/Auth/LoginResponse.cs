using System;
namespace Models.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        //public long ExpiresIn { get; set; }
    }
}
