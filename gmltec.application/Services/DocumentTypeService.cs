using AutoMapper;
using gmltec.application.Contracts.Services;
using gmltec.application.Contracts.UoW;
using gmltec.application.Dtos.DocumentType;
using gmltec.Domain.Entities;
using gmltec.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.application.Services
{
    public class DocumentTypeService : BaseService, IDocumentTypeService
    {
        public DocumentTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<HttpResponse<List<DocumentTypeDto>>> GetDocumentType()
        {
            try
            {
                IReadOnlyList<DocumentType> documentTypes = await _unitOfWork.documentTypeRepository.GetAllAsync();

                List<DocumentTypeDto> dtos = [.. documentTypes.Select(DocumentTypeDto (dt) => new DocumentTypeDto
                {
                    Id = dt.Id,
                    Name = dt.Name,
                    Abbreviation = dt.Abbreviation,

                })];

                return new HttpResponse<List<DocumentTypeDto>>
                {
                    Message = "Documentos obtenidos con exito",
                    HttpStatusCode = HttpStatusCode.OK,
                    Data = dtos
                };
            }
            catch (Exception ex)
            {
                return new HttpResponse<List<DocumentTypeDto>>(
                HttpStatusCode.InternalServerError,
                $"Error interno: {ex.Message}",
                [ex.StackTrace]
            );
            }
        }
    }
}
