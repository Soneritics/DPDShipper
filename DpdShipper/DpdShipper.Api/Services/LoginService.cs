using System.ServiceModel;
using DpdShipper.Api.Domain.Exceptions;
using DpdShipper.Api.Domain.Login;
using DpdShipper.Api.Endpoints;
using LoginService;

namespace DpdShipper.Api.Services;

public class LoginService : ILoginService
{
    private readonly IEndpoints _endpoints;

    internal LoginService(IEndpoints endpoints)
    {
        _endpoints = endpoints;
    }

    public async Task<AuthToken> LoginAsync(string delisId, string password)
    {
        var endpointAddress = new EndpointAddress(_endpoints.LoginService);
        var endpointConfiguration = LoginServiceSoapSoapClient.EndpointConfiguration.LoginServiceSoapSoap;

        var loginService = new LoginServiceSoapSoapClient(endpointConfiguration, endpointAddress);

        try
        {
            var loginResult = await loginService.getAuthAsync(delisId, password, "en_EN");

            return new AuthToken()
            {
                DelisId = loginResult.@return.delisId,
                Depot = loginResult.@return.depot,
                Token = loginResult.@return.authToken,
                TokenExpires = loginResult.@return.authTokenExpires
            };
        }
        catch (Exception ex)
        {
            throw new LoginFailedException("LoginAsync failed", ex);
        }
    }
}