using System;
namespace Models.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
