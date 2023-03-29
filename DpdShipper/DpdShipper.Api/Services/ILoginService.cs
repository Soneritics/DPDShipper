using DpdShipper.Api.Domain.Login;

namespace DpdShipper.Api.Services;

public interface ILoginService
{
    Task<AuthToken> LoginAsync(string delisId, string password);
}