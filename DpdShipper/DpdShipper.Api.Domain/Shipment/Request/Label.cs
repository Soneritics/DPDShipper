﻿namespace DpdShipper.Api.Domain.Shipment.Request;

public class Label
{
    public string? GeneralCustomerReferenceNumber { get; set; }
    public Products Product { get; set; } = Products.CL;
    public Sender Sender { get; set; }
    public Recipient Recipient { get; set; }
    public IEnumerable<Parcel> Parcels { get; set; }
}