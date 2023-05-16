using DpdShipper.Api.Domain.Shipment.Request;
using ShipmentService;

namespace DpdShipper.Api.Business;

public class LabelToOrderMapper : IMapper<IEnumerable<Label>, IEnumerable<order>, string>
{
    public IEnumerable<order> Map(IEnumerable<Label> from, string? extraMappingData)
    {
        var result = new List<order>();

        foreach (var label in from)
        {
            var generalShipmentData = new generalShipmentData()
            {
                mpsCustomerReferenceNumber1 = label.ShipmentReferenceNumber,
                product = label.Product == Products.CL ? "CL" : string.Empty,
                sendingDepot = extraMappingData ?? string.Empty,
                sender = new address()
                {
                    name1 = label.Sender.Name,
                    street = label.Sender.Street,
                    houseNo = label.Sender.HouseNumber,
                    zipCode = label.Sender.ZipCode,
                    city = label.Sender.City,
                    country = label.Sender.CountryCode
                },
                recipient = new address()
                {
                    name1 = label.Recipient.Name,
                    name2 = label.Recipient.Company ?? null,
                    street = label.Recipient.Street,
                    houseNo = label.Recipient.HouseNumber,
                    street2 = label.Recipient.Street2 ?? null,
                    zipCode = label.Recipient.ZipCode,
                    city = label.Recipient.City,
                    country = label.Recipient.CountryCode,
                    email = label.Recipient.Email ?? null,
                    type = label.Recipient.RecipientType == RecipientTypes.Business ? "B" : "P"
                }
            };

            var productAndServiceData = new productAndServiceData()
            {
                orderType = "consignment",
                saturdayDelivery = label.SaturdayDelivery,
                saturdayDeliverySpecified = true
            };

            var parcels = label.Parcels.Select(
                p => new parcels()
                {
                    customerReferenceNumber1 = p.ParcelSpecificReferenceNumber,
                    weight = p.Weight,
                    weightSpecified = true
                })
                .ToArray();

            result.Add(new order()
            {
                generalShipmentData = generalShipmentData,
                productAndServiceData = productAndServiceData,
                parcels = parcels
            });
        }

        return result;
    }
}