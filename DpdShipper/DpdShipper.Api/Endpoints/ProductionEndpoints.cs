namespace DpdShipper.Api.Endpoints;

internal class ProductionEndpoints : IEndpoints
{
    public string LoginService => "https://wsshipper.dpd.nl/PublicApi/services/LoginService/V2_1";
    public string ShipmentService => "https://wsshipper.dpd.nl/PublicApi/services/ShipmentService/V3_3";
}