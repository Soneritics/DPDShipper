﻿using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
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

var dummySender = new Sender()
{
    Name = "Paleis van Justitie",
    Street = "Prins Clauslaan",
    HouseNumber = "60",
    ZipCode = "2595AJ",
    City = "Den Haag",
    CountryCode = "NL"
};

var dummyRecipient = new Recipient()
{
    Name = "Het Torentje",
    Street = "Binnenhof",
    HouseNumber = "17",
    ZipCode = "2513 AA",
    City = "Den Haag",
    CountryCode = "NL",
    RecipientType = RecipientTypes.Personal
};

var labels = new List<Label>()
{
    new ()
    {
        ShipmentReferenceNumber = "SomeReferenceNumber01",
        Sender = dummySender,
        Recipient = dummyRecipient,
        Product = Products.CL,
        Parcels = new List<Parcel>()
        {
            new ()
            {
                ParcelSpecificReferenceNumber = "ParcelRefNr01",
                Weight = 2000
            },
            new ()
            {
                ParcelSpecificReferenceNumber = "ParcelRefNr02",
                Weight = 100
            }
        }
    },

    new ()
    {
        ShipmentReferenceNumber = "SomeReferenceNumber02",
        Sender = dummySender,
        Recipient = dummyRecipient,
        Product = Products.CL,
        Parcels = new List<Parcel>()
        {
            new ()
            {
                ParcelSpecificReferenceNumber = "ParcelRefNr03",
                Weight = 500
            }
        }
    },

    new ()
    {
        SaturdayDelivery = true,
        ShipmentReferenceNumber = "SaturdayDelivery01",
        Sender = dummySender,
        Recipient = dummyRecipient,
        Product = Products.CL,
        Parcels = new List<Parcel>()
        {
            new ()
            {
                ParcelSpecificReferenceNumber = "SaturdayDeliveryPrc",
                Weight = 500
            }
        }
    },

    new ()
    {
        ShipmentReferenceNumber = "SomeReferenceNumber03",
        Sender = dummySender,
        Recipient = new Recipient()
        {
            Name = "Het Torentje",
            Street = "Binnenhof",
            HouseNumber = "17",
            Street2 = "Bovenste kamertje",
            ZipCode = "2513 AA",
            City = "Den Haag",
            CountryCode = "NL",
            RecipientType = RecipientTypes.Personal
        },
        Product = Products.CL,
        Parcels = new List<Parcel>()
        {
            new ()
            {
                ParcelSpecificReferenceNumber = "ParcelRefNr04",
                Weight = 256
            }
        }
    },
};

var json = JsonSerializer.Serialize(labels);
var shipmentResult = await shipperApi
    .ShipmentService(authToken)
    .GetPdfAsync(labels, PaperFormats.A6);

await using var fileStream = File.Create($"file-dpd-{DateTime.Now.ToFileTime()}.pdf");
await fileStream.WriteAsync(shipmentResult.ResultFile);
