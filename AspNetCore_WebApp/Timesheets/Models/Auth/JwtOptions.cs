namespace Models.Auth
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public int AccessLifetime { get; set; }
        public int RefreshLifeTime { get; set; }
    }
}
