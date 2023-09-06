namespace Invoice_Api.Auth
{
    public class JwtConfig
    {
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public int Expiration { get; set; }
    }
}
