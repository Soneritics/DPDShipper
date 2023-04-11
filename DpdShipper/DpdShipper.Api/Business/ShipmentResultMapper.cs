using DpdShipper.Api.Domain.Shipment.Response;
using ShipmentService;

namespace DpdShipper.Api.Business;

public class ShipmentResultMapper : IMapper<storeOrdersResponse?, ShipmentResults, byte[]>
{
    public ShipmentResults Map(storeOrdersResponse? from, byte[]? extraMappingData)
    {
        return from == null
            ? new ShipmentResults()
            : MapStoreOrdersResponse(from, extraMappingData);
    }

    private ShipmentResults MapStoreOrdersResponse(storeOrdersResponse storeOrdersResponse, byte[]? fileData)
    {
        var result = new ShipmentResults()
        {
            
            ResultFile = fileData,
            ShipmentResultList = new List<ShipmentResult>()
        };

        foreach (var orderResponse in storeOrdersResponse.orderResult.shipmentResponses)
        {
            result.ShipmentResultList.Add(
                new ShipmentResult()
                {
                    Consignment = orderResponse.mpsId,
                    LabelNumbers = orderResponse.parcelInformation.Select(
                        p => p.parcelLabelNumber),
                    Errors = orderResponse.faults?.Select(
                        e => new ShipmentLabelError()
                        {
                            Code = e.faultCode,
                            Message = e.message
                        }) ?? new List<ShipmentLabelError>()
                    
                });
        }

        return result;
    }
}