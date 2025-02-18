using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface ICountriesService
    {
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);
        List<CountryResponse> GetAllcountry();
        CountryResponse? GetcountryByCountryID(Guid ?Countryid);
    }
}
