using AutoMapper;
using gmltec.application.Contracts.Services;
using gmltec.application.Contracts.UoW;
using gmltec.application.Dtos.Person;
using gmltec.application.Filters;
using gmltec.application.Util;
using gmltec.application.Validations;
using gmltec.Domain.Entities;
using gmltec.Domain.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.application.Services
{
    public class PersonService : BaseService, IPersonService
    {
        public PersonService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<HttpResponse<List<PersonDto>>> GetPerson(PaginationFilterRequest request)
        {
            try
            {
                Expression<Func<Person, bool>> filterExpression = ExpressionBuilder.BuildExpression<Person>(request.Filters);

                IReadOnlyList<Person> persons = await _unitOfWork.personRepository.GetAllAsync(filterExpression, x => x.DocumentType);

                int totalRecords = persons.Count;

                List<Person> paginatedData = persons
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                List<PersonDto> dtos = _mapper.Map<List<PersonDto>>(persons);

                return new HttpResponse<List<PersonDto>>
                {
                    Message = "Personas obtenidas con exito",
                    HttpStatusCode = HttpStatusCode.OK,
                    Data = dtos,
                    TotalRecords = totalRecords,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };
            }
            catch (Exception ex)
            {
                return new HttpResponse<List<PersonDto>>(
                    HttpStatusCode.InternalServerError,
                    $"Error interno: {ex.Message}",
                    [ex.StackTrace]
                );
            }
        }

        public async Task<HttpResponse<PersonDto>> GetPersonById(long id)
        {
            try
            {
                Person person = await _unitOfWork.personRepository.GetByIdWithIncludeAsync(x => x.Id == id && x.Active, x => x.DocumentType);

                if (person == null)
                {
                    return new HttpResponse<PersonDto>
                    {
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Message = "Persona no encontrada"
                    };
                }
                PersonDto dto = _mapper.Map<PersonDto>(person);

                return new HttpResponse<PersonDto>
                {
                    Message = "Persona obtenida con exito",
                    HttpStatusCode = HttpStatusCode.OK,
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                return new HttpResponse<PersonDto>(
                    HttpStatusCode.InternalServerError,
                    $"Error interno: {ex.Message}",
                    [ex.StackTrace]
                );
            }
        }

        public async Task<HttpResponse<PersonDto>> CreatePerson(PersonDto personDto)
        {
            try
            {
                PersonDtoValidator validator = new PersonDtoValidator();
                FluentValidation.Results.ValidationResult validationResult = validator.Validate(personDto);

                if (!validationResult.IsValid)
                {
                    return new HttpResponse<PersonDto>
                    {
                        Message = "Error en la validación de los datos.",
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    };
                }

                Person person = _mapper.Map<Person>(personDto);

                try
                {
                    await _unitOfWork.BeginTransactionAsync();

                    await _unitOfWork.personRepository.AddAsync(person);

                    await _unitOfWork.CommitAsync();
                }
                catch (Exception)
                {
                    await _unitOfWork.RollbackAsync();

                    throw;
                }

                PersonDto dto = _mapper.Map<PersonDto>(person);

                return new HttpResponse<PersonDto>
                {
                    Message = "Persona creada con éxito.",
                    HttpStatusCode = HttpStatusCode.Created,
                    Data = dto
                };
            }
            catch (Exception ex)
            {

                return new HttpResponse<PersonDto>
                {
                    Message = $"Error interno: {ex.Message}",
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = [ex.StackTrace]
                };
            }
        }


        public async Task<HttpResponse<PersonDto>> UpdatePerson(long id, PersonDto personDto)
        {
            try
            {
                PersonDtoValidator validator = new PersonDtoValidator();
                FluentValidation.Results.ValidationResult validationResult = validator.Validate(personDto);

                
                if (!validationResult.IsValid)
                {
                    return new HttpResponse<PersonDto>
                    {
                        Message = "Error en la validación de los datos.",
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    };
                }

                Person person = await _unitOfWork.personRepository.GetByIdWithoutExpressionAsync(id);

                if (person == null)
                {
                    return new HttpResponse<PersonDto>
                    {
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Message = "Persona no encontrada"
                    };
                }

                person.FirstName = personDto.Name;
                person.LastName = personDto.LastName;
                person.DocumentNumber = personDto.DocumentNumber;
                person.DocumentTypeId = personDto.DocumentTypeId;
                person.DateOfBirth = personDto.DateOfBirth;
                person.Salary = personDto.Salary;
                person.MaritalStatus = personDto.MaritalStatus;
                person.UpdatedDate = DateTime.Now;

                try
                {
                    await _unitOfWork.BeginTransactionAsync();
                    _unitOfWork.personRepository.Update(person);
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception)
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }

                PersonDto dto = _mapper.Map<PersonDto>(person);

                return new HttpResponse<PersonDto>
                {
                    Message = "Persona actualizada con éxito",
                    HttpStatusCode = HttpStatusCode.OK,
                    Data = dto
                };
            }
            catch (Exception ex)
            {

                return new HttpResponse<PersonDto>
                {
                    Message = $"Error interno: {ex.Message}",
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Errors = [ex.StackTrace]
                };
            }
        }


        public async Task<HttpResponse<bool>> DeletePerson(long id)
        {
            try
            {
                Person person = await _unitOfWork.personRepository.GetByIdWithoutExpressionAsync(id);

                if (person == null)
                {
                    return new HttpResponse<bool>
                    {
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Message = "Persona no encontrada"
                    };
                }

                try
                {
                    await _unitOfWork.BeginTransactionAsync();
                    await _unitOfWork.personRepository.MarkAsDeletedAsync(x => x.Id == id);

                    await _unitOfWork.CommitAsync();

                }
                catch (Exception)
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }

                

                return new HttpResponse<bool>
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Message = "Persona eliminada con exito."
                };

            }
            catch (Exception ex)
            {
                
                return new HttpResponse<bool>(
                    HttpStatusCode.InternalServerError,
                    $"Error interno: {ex.Message}",
                    [ex.StackTrace]
                );
            }
        }

    }
}
