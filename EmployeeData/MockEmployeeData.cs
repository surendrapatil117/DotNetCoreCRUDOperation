using DotNetCoreCRUDOperation.Data;
using DotNetCoreCRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreCRUDOperation.EmployeeData
{
    public class MockEmployeeData : IEmployeeData
    {
        // private readonly EmployeesDbContext emp_;

        //public MockEmployeeData(EmployeesDbContext edb)
        //{
        //    emp_ = edb;

        //}
        private EmployeesDbContext _context { get; set; }
        public MockEmployeeData(EmployeesDbContext context)
        {
            _context = context;
        }

        public MockEmployeeData()
        {
        }

        private List<Employee> Emp = new List<Employee>()
        {
        new Employee
        {
            id=Guid.NewGuid(),
            Name="Surendra",
            Address="Kolhapur",
            ContactNo="9955268574"
        },
        new Employee
        {
            id=Guid.NewGuid(),
            Name="Mukund",
            Address="Pune",
            ContactNo="9856236587"
        },
        new Employee
        {
            id=Guid.NewGuid(),
            Name="Dhanashree",
            Address="Nashik",
            ContactNo="8458965254"
        }
        };


        public Employee AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee EditEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        //public Employee GetEmployee(string name)
        //{
        //    return Emp.SingleOrDefault(x => x.Name == name);
        //}

        public Employee GetEmployee(Guid id)
        {
            return Emp.SingleOrDefault(x => x.id == id);
        }
        public void GetEmployeebydate(Parameter param)
        {
           // return Emp.SingleOrDefault(x => x.id == id);
        }

        public List<Employee> GetEmployees()
        {

            return _context.Employees.ToList();//.FirstOrDefault(e => e.Contactno == "9922736068").to;
            
      }

        public Employee GetEmployeebyname(string name)
        {

            return Emp.SingleOrDefault(x => x.Name == name); ;//.FirstOrDefault(e => e.Contactno == "9922736068").to;

        }



        public void Dispose()
        {
            _context?.Dispose();
        }

        List<Employeeautoid> IEmployeeData.GetEmployees()
        {
            return _context.Employeeautoids.ToList();
        }
    }
}
