using Entites;
using ServiceContracts.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool? ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(PersonResponse))
            {
                return false;
            }
            PersonResponse person = (PersonResponse)obj;
            return PersonID == this.PersonID
                && PersonName == person.PersonName
                && Email == person.Email
                && DateofBirth == person.DateofBirth
                && Gender == person.Gender
                && Address == person.Address
                && ReceiveNewsLetters == person.ReceiveNewsLetters;

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"person ID: {PersonID},person name{PersonName} , Email :{Email},Data of Birth:{DateofBirth?.ToString("dd MMMM yyyy")}" +
                $"Gender {Gender},CountryId:{CountryID} , Recive new letters {ReceiveNewsLetters}";
        }

        public PersonUpdateRequest TOpersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                PersonId = PersonID,
                PersonName = PersonName,
                Email = Email,
                DateofBirth = DateofBirth,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetters = ReceiveNewsLetters,

            };
        }
    }

    public static class PersonExtensions
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {

            return new PersonResponse()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateofBirth = person.DateofBirth,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Address = person.Address,
                CountryID = person.CountryID,
                Gender = person.Gender,
                Age = (person.DateofBirth != null) ? Math.Round((DateTime.Now - person.DateofBirth.Value).TotalDays / 365.25) : null
            };
        }

    };
}      
    






