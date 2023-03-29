using DpdShipper.Api.Domain.Login;
using DpdShipper.Api.Domain.Shipment.Request;
using DpdShipper.Api.Domain.Shipment.Response;

namespace DpdShipper.Api.Services;

public interface IShipmentService
{
    AuthToken AuthToken { get; set; }

    Task<ShipmentResponse> GetPdfAsync(IEnumerable<Label> labels, PaperFormats paperFormat);
}