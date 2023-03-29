using DpdShipper.Api;
using DpdShipper.Api.Domain.Shipment.Request;

Console.WriteLine("DPD Shipper API LoginAsync Example\n-----\n");

Console.Write("delisId: ");
var delisId = Console.ReadLine();

Console.Write("password: ");
var password = Console.ReadLine();

Console.WriteLine("\n");

var shipperApi = new DpdShipperApi(true);
var authToken = await shipperApi.LoginService.LoginAsync(delisId, password);

var labels = new List<Label>()
{
    // todo: generate random labels
};

await shipperApi.ShipmentService(authToken).GetPdfAsync(labels, PaperFormats.A6);

Console.ReadKey();
