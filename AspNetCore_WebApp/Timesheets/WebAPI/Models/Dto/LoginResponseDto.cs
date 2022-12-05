using System;
namespace Models.Dto
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        //public long ExpiresIn { get; set; }
    }
}
