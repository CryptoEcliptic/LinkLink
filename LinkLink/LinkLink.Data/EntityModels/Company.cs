using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkLink.Data.EntityModels
{
    public class Company
    {
        public Company()
        {
            this.Offices = new List<Office>();
        }

        [Key]
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<Office> Offices { get; set; }
    }
}
