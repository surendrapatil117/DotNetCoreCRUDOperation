using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreCRUDOperation.Models;

namespace DotNetCoreCRUDOperation.Data
{
    public class EmployeesDbContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employeeautoid> Employeeautoids { get; set; }
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options)
       : base(options)
        { }

    }
}
