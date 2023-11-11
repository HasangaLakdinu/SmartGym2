namespace AuthService.Configuration
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Aud1 { get; set; }
        public string Aud2 { get; set; }
    }
}
