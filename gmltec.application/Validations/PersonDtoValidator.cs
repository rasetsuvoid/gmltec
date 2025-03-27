using FluentValidation;
using gmltec.application.Dtos.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.application.Validations
{
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(50).WithMessage("El apellido no puede tener más de 50 caracteres.");

            RuleFor(p => p.DocumentNumber)
                .NotEmpty().WithMessage("El número de documento es obligatorio.")
                .Matches(@"^\d+$").WithMessage("El número de documento solo puede contener números.");

            RuleFor(p => p.DocumentTypeId)
                .NotEmpty().WithMessage("El tipo de documento es obligatorio.");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
                .LessThan(DateTime.Today).WithMessage("La fecha de nacimiento debe ser una fecha pasada.");

            RuleFor(p => p.Salary)
                .GreaterThan(0).WithMessage("El salario debe ser mayor a 0.");

            RuleFor(p => p.MaritalStatus)
                .NotNull().WithMessage("El estado civil es obligatorio.");
        }
    }
}
