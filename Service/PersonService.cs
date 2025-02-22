using Entites;
using Service.Helper;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PersonService : IpersonService
    {
        private readonly List<Person> _person;
        private readonly ICountriesService _countriesService;
        
        public PersonService(bool initialize=true)
        {
            _person = new List<Person>();
            _countriesService = new CountriesService();
            if(initialize)
            {
                
                _person.Add(new Person() { PersonID = Guid.Parse("3706F10C-9594-4167-BFC7-AB09CC1D42DA"), PersonName = "Aguste", Email = "aleddy0@booking.com", DateofBirth = DateTime.Parse("1993-01-02"), Gender = "Male", Address = "0858 Novick Terrace", ReceiveNewsLetters = false, CountryID = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B") });

                _person.Add(new Person() { PersonID = Guid.Parse("4D0A697E-13EA-4AFE-A74B-CB56B2E04FD3"), PersonName = "Jasmina", Email = "jsyddie1@miibeian.gov.cn", DateofBirth = DateTime.Parse("1991-06-24"), Gender = "Female", Address = "0742 Fieldstone Lane", ReceiveNewsLetters = true, CountryID = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F") });

                _person.Add(new Person() { PersonID = Guid.Parse("6435FA03-2A39-413F-9651-63CCC3C268D3"), PersonName = "Kendall", Email = "khaquard2@arstechnica.com", DateofBirth = DateTime.Parse("1993-08-13"), Gender = "Male", Address = "7050 Pawling Alley", ReceiveNewsLetters = false, CountryID = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F") });

                _person.Add(new Person() { PersonID = Guid.Parse("33282BF7-2A97-4C23-A650-06A14AA9009E"), PersonName = "Kilian", Email = "kaizikowitz3@joomla.org", DateofBirth = DateTime.Parse("1991-06-17"), Gender = "Male", Address = "233 Buhler Junction", ReceiveNewsLetters = true, CountryID = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E") });

                _person.Add(new Person() { PersonID = Guid.Parse("E88E4569-9895-416A-9757-38E000056494"), PersonName = "Dulcinea", Email = "dbus4@pbs.org", DateofBirth = DateTime.Parse("1996-09-02"), Gender = "Female", Address = "56 Sundown Point", ReceiveNewsLetters = false, CountryID = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E") });

                _person.Add(new Person() { PersonID = Guid.Parse("A795E22D-FAED-42F0-B134-F3B89B8683E5"), PersonName = "Corabelle", Email = "cadams5@t-online.de", DateofBirth = DateTime.Parse("1993-10-23"), Gender = "Female", Address = "4489 Hazelcrest Place", ReceiveNewsLetters = false, CountryID = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D") });

                _person.Add(new Person() { PersonID = Guid.Parse("ADB2D9CB-5B18-4390-830F-C79C23ED16EA"), PersonName = "Faydra", Email = "fbischof6@boston.com", DateofBirth = DateTime.Parse("1996-02-14"), Gender = "Female", Address = "2010 Farragut Pass", ReceiveNewsLetters = true, CountryID = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

                _person.Add(new Person() { PersonID = Guid.Parse("8ECE637D-C0DE-4F0A-8794-B16603F8CA93"), PersonName = "Oby", Email = "oclutheram7@foxnews.com", DateofBirth = DateTime.Parse("1992-05-31"), Gender = "Male", Address = "2 Fallview Plaza", ReceiveNewsLetters = false, CountryID = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

                _person.Add(new Person() { PersonID = Guid.Parse("501218BC-1808-458B-A0B7-36BA8C5D4C04"), PersonName = "Seumas", Email = "ssimonitto8@biglobe.ne.jp", DateofBirth = DateTime.Parse("1999-02-02"), Gender = "Male", Address = "76779 Norway Maple Crossing", ReceiveNewsLetters = false, CountryID = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

                _person.Add(new Person() { PersonID = Guid.Parse("A18F7F2A-A25D-4FB7-924F-535197B25FC3"), PersonName = "Freemon", Email = "faugustin9@vimeo.com", DateofBirth = DateTime.Parse("1996-04-27"), Gender = "Male", Address = "8754 Becker Street", ReceiveNewsLetters = false, CountryID = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });
            }
        }
        private PersonResponse COnVertPersonTOPerrsoRespones(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.
                GetcountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }
            ValidationHelper.ModelValidation(personAddRequest);
           
          
           
            Person person = personAddRequest.TOperson();
            person.PersonID = Guid.NewGuid();

            _person.Add(person);

            return COnVertPersonTOPerrsoRespones(person);

        }
 
        public List<PersonResponse> GetAllperson()
        {
           return  _person.Select(temp => temp.ToPersonResponse()).ToList(); 
        }

        public PersonResponse GetPersonBypersonId(Guid? personid)
        {
            if (personid == null)
                return null;

            Person? person = _person.FirstOrDefault(temp => temp.PersonID == personid);
            if (person == null)
                return null;

            return person.ToPersonResponse(); ;
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllperson();
            List<PersonResponse> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingPersons;

            switch (searchBy)
            {
                case nameof(PersonResponse.PersonName):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.PersonName) ?
                    temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.Email):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ?
                    temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                case nameof(PersonResponse.DateofBirth):
                    matchingPersons = allPersons.Where(temp =>
                    (temp.DateofBirth != null) ?
                    temp.DateofBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(PersonResponse.Gender):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Gender) ?
                    temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.CountryID):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ?
                    temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.Address):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ?
                    temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                default: matchingPersons = allPersons; break;
            }
            return matchingPersons;
        }

        public List<PersonResponse> GetSortedPerson(List<PersonResponse> Allpersons, string sortBy, SortOrderOptions sortorder)
        {
           if(string.IsNullOrEmpty(sortBy)) 
                    return Allpersons;
            List<PersonResponse> sortedPerson = (sortBy, sortorder)
             switch
            {
                (nameof(PersonResponse.PersonName),SortOrderOptions.Asc)=>
                Allpersons.OrderBy(temp=> temp.PersonName,StringComparer.OrdinalIgnoreCase).ToList(),


                (nameof(PersonResponse.PersonName), SortOrderOptions.desc) =>
                Allpersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.Asc) =>
                Allpersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.desc) =>
                Allpersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateofBirth), SortOrderOptions.Asc) =>
                Allpersons.OrderBy(temp => temp.DateofBirth).ToList(),

                (nameof(PersonResponse.DateofBirth), SortOrderOptions.desc) =>
                Allpersons.OrderByDescending(temp => temp.DateofBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.Asc) =>
                Allpersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.desc) =>
                Allpersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.Asc) =>
                Allpersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.desc) =>
                Allpersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.Asc) =>
                Allpersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.desc) =>
                Allpersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.Asc) =>
                Allpersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.desc) =>
                Allpersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.Asc) =>
               Allpersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.desc) =>
                Allpersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                _=>Allpersons 



            };
            return sortedPerson;

        }

     

         public  PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(Person));

            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            //get matching person object to update
            Person? matchingPerson = _person.FirstOrDefault(temp => temp.PersonID == personUpdateRequest.PersonId);
            if (matchingPerson == null)
            {
                throw new ArgumentException("Given person id doesn't exist");
            }

            //update all details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateofBirth = personUpdateRequest.DateofBirth;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            return matchingPerson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? personid)
        {
            if(personid == null) throw new ArgumentNullException(nameof(personid));


            Person person=_person.FirstOrDefault(temp => temp.PersonID == personid);



            if(person == null)
                return false;

            _person.RemoveAll(temp=> temp.PersonID == personid);
            return true;
        }
    }
 }


