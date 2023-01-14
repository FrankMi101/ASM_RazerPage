namespace ASM.Models
{
    public class AppConfigJwtSettings
    {
        public string JWTSecret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}