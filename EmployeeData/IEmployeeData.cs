using DotNetCoreCRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreCRUDOperation.EmployeeData
{
    public interface IEmployeeData
    {
         List<Employeeautoid> GetEmployees();

         Employee GetEmployee(Guid id);

        Employee AddEmployee(Employee employee);

        void DeleteEmployee(Employee employee);
        Employee EditEmployee(Employee employee);
        void GetEmployeebydate(Parameter param);

    }
}
