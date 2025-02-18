using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IpersonService
    {
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);
        List<PersonResponse> GetAllperson();
        PersonResponse? GetPersonBypersonId(Guid? personid);
        List<PersonResponse>GetFilteredPersons(string search, string? searchString);
        List<PersonResponse> GetSortedPerson(List<PersonResponse> Allpersons , string sortBy , SortOrderOptions sortorder);

        PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);

        bool DeletePerson(Guid? personid);


    }
}
