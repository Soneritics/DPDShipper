namespace DpdShipper.Api.Endpoints;

internal class ProductionEndpoints : IEndpoints
{
    public string LoginService => "https://wsshipper.dpd.nl/soap/services/LoginService/V2_1";

    public string ShipmentService => "https://wsshipper.dpd.nl/soap/services/ShipmentService/V3_3/";
}