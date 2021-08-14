using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreCRUDOperation.Contact;
using DotNetCoreCRUDOperation.Data;
using DotNetCoreCRUDOperation.EmployeeData;
using DotNetCoreCRUDOperation.EmployeeHelper;
using DotNetCoreCRUDOperation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetCoreCRUDOperation.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeData _employeeData;
        // public EmployeesDbContext obj = new EmployeesDbContext();
        private EmployeesDbContext obj;
        Dictionary<string, string> Responsestatus = new Dictionary<string, string>();
        public EmployeesController(IEmployeeData employeeData, EmployeesDbContext _obj)
        {
            _employeeData = employeeData;
            obj = _obj;

        }
        // [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetAllEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }
        [Route("api/[controller]/{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            var emp = _employeeData.GetEmployee(id);
            if (emp != null)
            {
                return Ok(emp);
            }
            else
            {
                return NotFound($"Employee With the name:{id} was not found");
            }
        }

        [HttpGet]
        [Route("api/[controller]/{action}")]
        public IActionResult GetEmployeebydate(string firstName, string lastName)
        {
            // return Ok("Success");
            return Ok(_employeeData.GetEmployees().Where(x => x.Name == firstName & x.Address == lastName));
        }

        [HttpGet]
        [Route("api/[controller]/{action}/{employeeid}")]
        public IActionResult GetEmployeebyid(string employeeid)
        {
            var emp = new Employeeautoid();
            using (var db = obj)
            {
                emp = db.Employeeautoids.Where(x => x.id == int.Parse(employeeid)).FirstOrDefault();
            }
            return Ok(emp);
            // return Ok(_employeeData.GetEmployees().Where(x => x.id== int.Parse(employeeid) ));
        }


        [HttpGet]
        [Route("api/[controller]/{action}")]
        public IActionResult UpdateGetEmployeebydate(string firstName, string lastName)
        {
            // MockEmployeeData mdata = new MockEmployeeData(); 
            List<Employee> emp = new List<Employee>();
            //  emp = mdata.GetEmployeebyname(firstName);

            using (var db = obj)
            {
                emp = db.Employees.Where(x => x.Name == firstName).ToList();
                // emp = db.Employees.FirstOrDefault(x => x.Name == firstName);
                foreach (var item in emp)
                {
                    item.Address = "Updated";
                    db.Employees.Attach(item);
                    db.Attach(item).Property(x => x.Address).IsModified = true;
                    db.SaveChanges();
                }



            }



            return Ok("Success");
            //  _employeeData.GetEmployees().Where(x => x.Name == firstName & x.Address == lastName);


        }

        [HttpGet]
        [Route("api/[controller]/{action}")]
        public IActionResult UpdateSingleEmployeebydate(string firstName, string lastName)
        {

            using (var db = obj)
            {
                var emp = db.Employees.FirstOrDefault(x => x.Name == firstName);
                emp.Address = "Updated";
                db.Employees.Attach(emp);
                db.Attach(emp).Property(x => x.Address).IsModified = true;
                db.SaveChanges();

            }

            return Ok("Success");
            //  _employeeData.GetEmployees().Where(x => x.Name == firstName & x.Address == lastName);


        }
        [HttpPost]
        [Route("api/[controller]/{action}")]
        public IActionResult InsertEmployee([FromBody] EmployeeValid emp)
        {

            //string name,string address,string contactno,string pincodee
            //Employee emp = new Employee();
            //emp.name = name;
            //emp.address = address;
            //emp.contactno = contactno;
            //emp.pincodee = pincodee;


            using (var db = obj)
            {
                if (ModelState.IsValid)
                {
                    var empdb = ConvertEmployeeValidto_Employee.ConvertEmployeeValidtoEmployee(emp);
                    //  db.Employees.Add(empdb);
                    db.Employeeautoids.Add(empdb);
                    db.SaveChanges();
                    Responsestatus.Add("Status", "Success");

                }



            }



            return Ok(Responsestatus);
            //  _employeeData.GetEmployees().Where(x => x.Name == firstName & x.Address == lastName);


        }

        [HttpPut]
        [Route("api/[controller]/{action}")]
        public IActionResult Updateemployee([FromBody] EmployeeValid empdb)
        {
            // Employee emp
            using (var db = obj)
            {
                // var emp = db.Employeeautoids.FirstOrDefault(x => x.id == empdb.id);
                var emp = ConvertEmployeeValidto_Employee.ConvertEmployeeValidtoEmployee(empdb);
                db.Employeeautoids.Attach(emp);
                // db.Employees.Attach(emp);
                // emp.Name = empdb.Name;
                // emp.ContactNo = empdb.ContactNo;
                // emp.Pincodee = empdb.Pincodee;
                db.Attach(emp).Property(x => x.Address).IsModified = true;
                db.Attach(emp).Property(x => x.ContactNo).IsModified = true;
                db.Attach(emp).Property(x => x.Name).IsModified = true;
                db.Entry(emp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                Responsestatus.Add("Status", "Updated");
            }

            return Ok(Responsestatus);
            //  _employeeData.GetEmployees().Where(x => x.Name == firstName & x.Address == lastName);


        }

        [HttpGet("gettoken")]
        [Route("api/[controller]/{action}")]
        public Object GetToken()
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "http://mysite.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "bilal"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            //Adding JWT token to Cookies
            Response.Cookies.Append(
                "X-Access-Token",
                jwt_token,
                new CookieOptions()
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true,
                    MaxAge = System.TimeSpan.FromSeconds(72000)

                });

            //Adding JWT token to Cookies
            Response.Cookies.Append(
       "AccessToken",
       jwt_token,
       new CookieOptions()
       {
           Path = "/",
           HttpOnly = false,
           Secure = false,
           Expires = DateTimeOffset.Now.AddMinutes(20)
       }
   ) ;
            // Set("AccessToken", jwt_token, 10000);

            //CookieOptions option = new CookieOptions();
            //option.Expires = DateTime.Now.AddMilliseconds(10);
            //option.Secure = true;
            //Response.Cookies.Append("AccessTokenNew", jwt_token, option);
            return new { data = jwt_token };
        }

        [HttpPost("getname1")]
        [Route("api/[controller]/{action}")]
        public String GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }

        [Authorize]
        [HttpPost("getname2")]
        [Route("api/[controller]/{action}")]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };

            }
            return null;
        }





    }
}
