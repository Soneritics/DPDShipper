namespace DpdShipper.Api.Domain.Login;

public class AuthToken
{
    public string DelisId { get; set; }
    public string Depot { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpires { get; set; }
}
