using DpdShipper.Api.Domain.Login;
using DpdShipper.Api.Endpoints;
using DpdShipper.Api.Services;

namespace DpdShipper.Api;

public class DpdShipperApi
{
    private readonly IEndpoints _endpoints;

    public DpdShipperApi(bool test = false)
    {
        _endpoints = test
            ? new TestEndpoints()
            : new ProductionEndpoints();
    }

    private ILoginService? _loginService;
    public ILoginService LoginService => _loginService ??= new Services.LoginService(_endpoints);

    public IShipmentService ShipmentService(AuthToken authToken) => new Services.ShipmentService(_endpoints, authToken);
}