﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;


namespace CRUDExample.Controllers
{
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        //private fields
        private readonly IpersonService _personsService;
        private readonly ICountriesService _countriesService;

        //constructor
        public PersonsController(IpersonService personsService, ICountriesService countriesService)
        {
            _personsService = personsService;
            _countriesService = countriesService;
        }

        //Url: persons/index
        [Route("[action]")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.Asc)
        {
            //Search
            ViewBag.SearchFields = new Dictionary<string, string>()
      {
        { nameof(PersonResponse.PersonName), "Person Name" },
        { nameof(PersonResponse.Email), "Email" },
        { nameof(PersonResponse.DateofBirth), "Date of Birth" },
        { nameof(PersonResponse.Gender), "Gender" },
        { nameof(PersonResponse.CountryID), "Country" },
        { nameof(PersonResponse.Address), "Address" }
      };
            List<PersonResponse> persons = _personsService.GetFilteredPersons(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //Sort
            List<PersonResponse> sortedPersons = _personsService.GetSortedPerson(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedPersons); //Views/Persons/Index.cshtml
        }


        //Executes when the user clicks on "Create Person" hyperlink (while opening the create view)
        //Url: persons/create
        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            List<CountryResponse> countries = _countriesService.GetAllcountry();
            ViewBag.Countries = countries.Select(temp =>
              new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() }
            );

            //new SelectListItem() { Text="Harsha", Value="1" }
            //<option value="1">Harsha</option>
            return View();
        }

        [HttpPost]
        //Url: persons/create
        [Route("[action]")]
        public IActionResult Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = _countriesService.GetAllcountry();
                ViewBag.Countries = countries.Select(temp =>
                new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            //call the service method
            PersonResponse personResponse = _personsService.AddPerson(personAddRequest);

            //navigate to Index() action method (it makes another get request to "persons/index"
            return RedirectToAction("Index", "Persons");
        }

        [HttpGet]
        [Route("[action]/{personID}")] //Eg: /persons/edit/1
        public IActionResult Edit(Guid personID)
        {
            PersonResponse? personResponse = _personsService.GetPersonBypersonId(personID);
            if (personResponse == null)
            {
                return RedirectToAction("Index");
            }

            PersonUpdateRequest personUpdateRequest = personResponse.TOpersonUpdateRequest();

            List<CountryResponse> countries = _countriesService.GetAllcountry();
            ViewBag.Countries = countries.Select(temp =>
            new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

            return View(personUpdateRequest);
        }


        [HttpPost]
        [Route("[action]/{personID}")]
        public IActionResult Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = _personsService.GetPersonBypersonId(personUpdateRequest.PersonId);

            if (personResponse == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                PersonResponse updatedPerson = _personsService.UpdatePerson(personUpdateRequest);
                return RedirectToAction("Index");
            }
            else
            {
                List<CountryResponse> countries = _countriesService.GetAllcountry();
                ViewBag.Countries = countries.Select(temp =>
                new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(personResponse.TOpersonUpdateRequest());
            }
        }


        [HttpGet]
        [Route("[action]/{personID}")]
        public IActionResult Delete(Guid? personID)
        {
            PersonResponse? personResponse = _personsService.GetPersonBypersonId(personID);
            if (personResponse == null)
                return RedirectToAction("Index");

            return View(personResponse);
        }

        [HttpPost]
        [Route("[action]/{personID}")]
        public IActionResult Delete(PersonUpdateRequest personUpdateResult)
        {
            PersonResponse? personResponse = _personsService.GetPersonBypersonId(personUpdateResult.PersonId);
            if (personResponse == null)
                return RedirectToAction("Index");

            _personsService.DeletePerson(personUpdateResult.PersonId);
            return RedirectToAction("Index");
        }
    }
}