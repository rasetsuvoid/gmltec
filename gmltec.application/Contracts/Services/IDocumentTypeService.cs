using gmltec.application.Dtos.DocumentType;
using gmltec.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.application.Contracts.Services
{
    public interface IDocumentTypeService
    {
        Task<HttpResponse<List<DocumentTypeDto>>> GetDocumentType();
    }
}
