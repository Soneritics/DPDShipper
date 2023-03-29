namespace DpdShipper.Api.Business;

public interface IMapper<TFrom, TTo, TExtraMappingData>
{
    TTo Map(TFrom from, TExtraMappingData? extraMappingData);
}