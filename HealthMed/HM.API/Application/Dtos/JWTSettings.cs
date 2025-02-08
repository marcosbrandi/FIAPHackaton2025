namespace HM.API.Application.Dtos
{
    public class JwtSettings //(string key, string issuer, string audience)
    {
        public string Key { get; } //= key;
        public string Issuer { get; } //= issuer;
        public string Audience { get; } //= audience;
    }
}
