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
        
        public PersonService()
        {
            _person = new List<Person>();
            _countriesService = new CountriesService();
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
                case nameof(Person.PersonName):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.PersonName) ?
                    temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ?
                    temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                case nameof(Person.DateofBirth):
                    matchingPersons = allPersons.Where(temp =>
                    (temp.DateofBirth != null) ?
                    temp.DateofBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Gender) ?
                    temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.CountryID):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ?
                    temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Address):
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


