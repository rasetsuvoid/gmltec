using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.application.Dtos.Person
{
    public class PersonDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public long DocumentTypeId { get; set; }
        public string? DocumentType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }
        public bool MaritalStatus { get; set; }
    }
}
