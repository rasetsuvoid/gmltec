using gmltec.application.Contracts.Services;
using gmltec.application.Dtos.DocumentType;
using gmltec.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace gmltec.web.Controllers
{
    public class DocumentTypeController : BaseController
    {
        private readonly IDocumentTypeService _documentTypeService;

        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }
        [HttpGet("GetDocumentType")]
        public async Task<IActionResult> GetDocumentTypeAsync()
        {
            HttpResponse<List<DocumentTypeDto>> result = await _documentTypeService.GetDocumentType();
            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
