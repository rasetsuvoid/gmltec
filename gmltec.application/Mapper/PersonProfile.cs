using AutoMapper;
using gmltec.application.Dtos.Person;
using gmltec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.application.Mapper
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            // De PersonDto a Person
            CreateMap<PersonDto, Person>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DocumentType, opt => opt.Ignore()); // DocumentType se maneja por ID

            // De Person a PersonDto
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType != null ? src.DocumentType.Name : null));
        }
    }

}
