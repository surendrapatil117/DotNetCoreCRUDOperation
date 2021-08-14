using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreCRUDOperation.Models
{
    public class Employee
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
       
        public string Pincodee { get; set; }
       // public string other { get; set; }
    }
}
