namespace DpdShipper.Api.Domain.Shipment.Response;

public class ShipmentResult
{
    public string DpdMpsId { get; set; }

    public IEnumerable<string> LabelNumbers { get; set; }

    public IEnumerable<ShipmentLabelError> Errors { get; set; }
}