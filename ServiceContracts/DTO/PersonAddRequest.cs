﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites;
using ServiceContracts.DTO.Enums;
namespace ServiceContracts.DTO
{
     public class PersonAddRequest
     {

        [Required(ErrorMessage ="Person Name Can't be blank")]

        public string? PersonName { get; set; }
        [Required(ErrorMessage ="Email can't be blank")]
        [EmailAddress(ErrorMessage ="Email value should be valid Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateofBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool? ReceiveNewsLetters { get; set; }

        public Person TOperson()
        {
            return new Person()
            {  
                
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
   