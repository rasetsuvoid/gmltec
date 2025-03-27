using gmltec.application.Dtos.Person;
using gmltec.application.Filters;
using gmltec.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.application.Contracts.Services
{
    public interface IPersonService
    {
        Task<HttpResponse<List<PersonDto>>> GetPerson(PaginationFilterRequest request);
        Task<HttpResponse<PersonDto>> GetPersonById(long id);
        Task<HttpResponse<PersonDto>> CreatePerson(PersonDto personDto);
        Task<HttpResponse<PersonDto>> UpdatePerson(long id, PersonDto personDto);
        Task<HttpResponse<bool>> DeletePerson(long id);
    }
}
