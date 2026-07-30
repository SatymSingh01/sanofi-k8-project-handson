namespace LibAppBase.Configuration;

public class JwtSettings
{
    public const string SectionName = "Jwt";

    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = "AuthSite";
    public string Audience { get; set; } = "DemoPlatform";
    public int ExpirationMinutes { get; set; } = 60;
}
