using gmltec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.application.Contracts.Persistence
{
    public interface IPersonRepository : IRepository<Person>
    {
    }
}
