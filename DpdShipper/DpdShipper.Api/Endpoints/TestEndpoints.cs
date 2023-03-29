namespace DpdShipper.Api.Endpoints;

internal class TestEndpoints : IEndpoints
{
    public string LoginService => "https://wsshippertest.dpd.nl/PublicApi/services/LoginService/V2_1";
    public string ShipmentService => "https://wsshippertest.dpd.nl/PublicApi/services/ShipmentService/V3_3";
}