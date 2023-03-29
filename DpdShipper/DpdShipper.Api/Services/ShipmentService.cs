using System.ServiceModel;
using DpdShipper.Api.Business;
using DpdShipper.Api.Domain.Exceptions;
using DpdShipper.Api.Domain.Login;
using DpdShipper.Api.Domain.Shipment.Request;
using DpdShipper.Api.Domain.Shipment.Response;
using DpdShipper.Api.Endpoints;
using ShipmentService;

namespace DpdShipper.Api.Services;

public class ShipmentService : IShipmentService
{
    private readonly IEndpoints _endpoints;
    private readonly IMapper<IEnumerable<Label>, IEnumerable<order>, string> _labelToOrderMapper;
    private readonly IMapper<storeOrdersResponse?, ShipmentResults, byte[]> _shipmentResultMapper;

    public AuthToken AuthToken { get; set; }

    internal ShipmentService(
        IEndpoints endpoints,
        AuthToken authToken,
        IMapper<IEnumerable<Label>, IEnumerable<order>, string> labelToOrderMapper,
        IMapper<storeOrdersResponse?, ShipmentResults, byte[]> shipmentResultMapper)
    {
        _endpoints = endpoints;
        AuthToken = authToken;
        _labelToOrderMapper = labelToOrderMapper;
        _shipmentResultMapper = shipmentResultMapper;
    }

    internal ShipmentService(IEndpoints endpoints, AuthToken authToken)
        : this(endpoints, authToken, new LabelToOrderMapper(), new ShipmentResultMapper())
    {
    }

    public async Task<ShipmentResults> GetPdfAsync(IEnumerable<Label> labels, PaperFormats paperFormat)
    {
        var response = await GetResponseAsync(labels, paperFormat, PrintTypes.Pdf);
        return _shipmentResultMapper.Map(response, response?.orderResult?.parcellabelsPDF ?? null);
    }

    private async Task<storeOrdersResponse?> GetResponseAsync(IEnumerable<Label> labels, PaperFormats paperFormat, PrintTypes printType)
    {
        var paperFormatString = paperFormat == PaperFormats.A4 ? "A4" : paperFormat == PaperFormats.A6 ? "A6" : string.Empty;
        var printTypeString = printType == PrintTypes.Pdf ? "PDF" : string.Empty;

        var shipmentService = new ShipmentServiceSoap33SoapClient(
            ShipmentServiceSoap33SoapClient.EndpointConfiguration.ShipmentServiceSoap33Soap,
            new EndpointAddress(_endpoints.ShipmentService));

        var authentication = new authentication()
        {
            delisId = AuthToken.DelisId,
            authToken = AuthToken.Token,
            messageLanguage = "en_EN"
        };

        var printOptions = new printOptions()
        {
            paperFormat = paperFormatString,
            printerLanguage = printTypeString
        };

        var orders = _labelToOrderMapper.Map(labels, AuthToken.Depot);

        try
        {
            return await shipmentService.storeOrdersAsync(
                authentication,
                printOptions,
                orders.ToArray());
        }
        catch (Exception ex)
        {
            // Unfortunately the DPD webservice uses an old ASMX service, which
            // sends an exception that does not exist anymore.
            // Being unable to parse the SoapException's Detail property, it's
            // impossible to determine the exact problem.
            throw new LabelGeneratingFailedException(ex.Message, ex);
        }
    }
}