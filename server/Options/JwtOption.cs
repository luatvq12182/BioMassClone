namespace server.Options
{
    public class JwtOption
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double ExpiryMinutes { get; set; }
        public double RefreshTokenExpiryMinutes { get; set; }
    }
}
