using System;
namespace Models.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
        public byte[] Salt { get; set; }
    }
}
