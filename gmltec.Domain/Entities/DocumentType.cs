﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.Domain.Entities
{
    public class DocumentType : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
