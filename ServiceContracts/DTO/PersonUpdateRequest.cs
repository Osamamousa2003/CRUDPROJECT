using Entites;
using ServiceContracts.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage = "Person id Can't be blank")]
        public  Guid PersonId {  get; set; }
        [Required(ErrorMessage = "Person Name Can't be blank")]
        public string? PersonName { get; set; }
        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email value should be valid Email")]
        public string? Email { get; set; }
        public DateTime? DateofBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool? ReceiveNewsLetters { get; set; }

        public Person TOperson()
        {
            return new Person()
            {  PersonID=PersonId,
                PersonName = PersonName,
                Email = Email, 
                DateofBirth = DateofBirth,
                Gender = Gender.ToString(),
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetters = ReceiveNewsLetters,

            };
        }
    }
}
