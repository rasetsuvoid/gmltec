using gmltec.application.Contracts.Services;
using gmltec.application.Dtos.Person;
using gmltec.application.Filters;
using gmltec.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace gmltec.web.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpPost("GetPerson")]
        public async Task<IActionResult> GetPersonAsync(PaginationFilterRequest filter)
        {
            HttpResponse<List<PersonDto>> result = await _personService.GetPerson(filter);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpGet("GetPersonById/{id}")]
        public async Task<IActionResult> GetPersonByIdAsync(long id)
        {
            HttpResponse<PersonDto> result = await _personService.GetPersonById(id);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpDelete("DeletePerson/{id}")]
        public async Task<IActionResult> DeletePersonAsync(long id)
        {
            HttpResponse<bool> result = await _personService.DeletePerson(id);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpPost("CreatePerson")]
        public async Task<IActionResult> CreatePersonAsync([FromBody] PersonDto personDto)
        {
            HttpResponse<PersonDto> result = await _personService.CreatePerson(personDto);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpPut("UpdatePerson/{id}")]
        public async Task<IActionResult> UpdatePersonAsync(long id, [FromBody] PersonDto personDto)
        {
            HttpResponse<PersonDto> result = await _personService.UpdatePerson(id, personDto);
            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
