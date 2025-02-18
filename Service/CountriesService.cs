using Entites;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Service
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {

            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest.CountryName));
            }
            if (_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentNullException("given country already have exists");
            }
            Country country = countryAddRequest.ToCountry();
            country.CountryId = Guid.NewGuid();
            _countries.Add(country);
            return country.TOCountryResponse();
        }

        public List<CountryResponse> GetAllcountry()
        {
            return _countries.Select(country => country.TOCountryResponse()).ToList();
        }

        public CountryResponse? GetcountryByCountryID(Guid? Countryid)
        {
            if (Countryid == null)
            
                return null;
                Country? country_response_from_list =
                     _countries.FirstOrDefault(temp => temp.CountryId == Countryid);
                if (country_response_from_list == null)
                    return null;

               return country_response_from_list.TOCountryResponse();
            
            
          }
}  }
    

