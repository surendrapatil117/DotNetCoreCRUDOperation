using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreCRUDOperation.Contact
{
    public class EmployeeValid
    {
        [Required(ErrorMessage = "Id Should be entered")]
        // public Guid id { get; set; }
        public int id { get; set; }
        [Required(ErrorMessage = "Name Should be entered")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address Should be entered")]
        public string Address { get; set; }
        [Required(ErrorMessage = "ContactNo Should be entered")]
        [MaxLength(12)]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Pincode Should be entered")]
        public string Pincodee { get; set; }
        // public string other { get; set; }
    }
}
