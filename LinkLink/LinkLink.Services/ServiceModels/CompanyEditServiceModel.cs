﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LinkLink.Services.ServiceModels
{
    public class CompanyEditServiceModel
    {
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
