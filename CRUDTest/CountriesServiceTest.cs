using System;
using System.Collections.Generic;
using Entites;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using Xunit;

namespace CRUDTest
{
    public  class CountriesServiceTest
    {
        
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService(false);
        }
        [Fact]
        public void AddCountry_NullCountry()
        {
            CountryAddRequest request = null;
            Assert.Throws<ArgumentNullException>(() =>
           {  _countriesService.AddCountry(request); });
        }
        [Fact]
        public void AddCountry_CountryNameCountry()
        {
            CountryAddRequest request = new CountryAddRequest()
            { CountryName = null };
            Assert.Throws<ArgumentNullException>(() =>
            { _countriesService.AddCountry(request); });
        }
        [Fact]
        public void AddCountry_DuplicateCountryNameCountry()
        {
            CountryAddRequest request1 = new CountryAddRequest()
            { CountryName = "USA" };
            CountryAddRequest request2 = new CountryAddRequest()
            { CountryName = "USA" };
            Assert.Throws<ArgumentNullException>(() =>
            { _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }
        [Fact]
        public void AddCountry_propNameCountry()
        {
            CountryAddRequest? request = new CountryAddRequest()
            { CountryName = "Japan" };
            
             CountryResponse response= _countriesService.AddCountry(request);
            List<CountryResponse>countries_from_GetAllCountries= _countriesService.GetAllcountry();
             Assert.True(response.CountryId!=Guid.Empty);
             Assert.Contains(response, countries_from_GetAllCountries);
        }
        [Fact]
        public void GetAllCountries_EmptyList()
        {
           List<CountryResponse>actual_countries_response_list= _countriesService.GetAllcountry();
            Assert.Empty(actual_countries_response_list);
        }
        [Fact]
        public void GetAllCountries_AddfewCountries()
        {
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>
            {
                new CountryAddRequest{CountryName="usa"},
                new CountryAddRequest{CountryName="uk"},
            };

            List<CountryResponse> country_list_from_add_country = new List<CountryResponse>();
            foreach (CountryAddRequest country_request in country_request_list)
            {
                country_list_from_add_country.Add( _countriesService.AddCountry(country_request));
            }

            List<CountryResponse> actualcountryResponsesList = _countriesService.GetAllcountry();

            foreach(CountryResponse expected_country in country_list_from_add_country)
            {
                Assert.Contains(expected_country, actualcountryResponsesList);
            }
    
        }

        [Fact]
        public void GetCountryByCoountryID_NullCountryID()
        {
            Guid? countryID = null;
           CountryResponse  country_response_from_get_method=
                _countriesService.GetcountryByCountryID(countryID);

            Assert.Null(country_response_from_get_method);
        }

        [Fact]
        public void GetCountryByCountryID_validCountryID()
        {

            CountryAddRequest? country_add_request = new CountryAddRequest()
            {
                CountryName = "China"
            };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);


            CountryResponse country_response_from_get_method = 
                _countriesService.GetcountryByCountryID(country_response_from_add.CountryId);

            Assert.Equal(country_response_from_add, country_response_from_get_method);
           

        }
    }

}
