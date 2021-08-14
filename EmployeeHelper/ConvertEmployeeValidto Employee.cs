using DotNetCoreCRUDOperation.Contact;
using DotNetCoreCRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreCRUDOperation.EmployeeHelper
{
    public static class ConvertEmployeeValidto_Employee
    {
        public static Employeeautoid ConvertEmployeeValidtoEmployee(EmployeeValid empmodel)
        {
            //Employee
            //  Employee dbemp = new Employee();
            Employeeautoid dbemp = new Employeeautoid();
            dbemp.id = empmodel.id;
            dbemp.Address = empmodel.Address;
            dbemp.ContactNo = empmodel.ContactNo;
            dbemp.Name = empmodel.Name;
            dbemp.Pincodee = empmodel.Pincodee;

            return dbemp;




        }
    }
}
