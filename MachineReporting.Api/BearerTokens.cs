public class BearerTokens
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string AccessTokenExpirationHours { get; set; }
    public string RefreshTokenExpirationHours { get; set; }
    public string AllowMultipleLoginsFromTheSameUser { get; set; }
    public string AllowSignoutAllUserActiveClients { get; set; }
}