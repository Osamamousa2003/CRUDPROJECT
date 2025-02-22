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

        public CountriesService(bool initialize=true)
        {
            _countries = new List<Country>();
            if (initialize)
            {
                _countries.AddRange(new List<Country>(){
                new Country() { CountryId = Guid.Parse("6042A39D-7DFF-4C99-96FE-CD4AE2E493D0"), CountryName = "USA" },
                new Country() { CountryId = Guid.Parse("8EB7C9FE-0EEC-4681-A63C-03062C78AF39"), CountryName = "Canada" },
                new Country() { CountryId = Guid.Parse("25E1CAD9-BB21-44EB-9CE6-96E966AE7831"), CountryName = "UK" },
                new Country() { CountryId = Guid.Parse("2B897B1C-D40F-43B8-90CE-11889E5B2B13"), CountryName = "India" },
                new Country() { CountryId = Guid.Parse("D96FC059-DBC4-4749-BB88-5149F8E6C4BE"), CountryName = "Australia" },
                }); 
                
            }
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
    

