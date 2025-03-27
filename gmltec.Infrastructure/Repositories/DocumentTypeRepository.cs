﻿using gmltec.application.Contracts.Persistence;
using gmltec.Domain.Entities;
using gmltec.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.Infrastructure.Repositories
{
    public class DocumentTypeRepository : Repository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
