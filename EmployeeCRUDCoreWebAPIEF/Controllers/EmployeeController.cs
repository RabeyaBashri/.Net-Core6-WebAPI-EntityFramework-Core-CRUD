using EmployeeCRUDCoreWebAPIEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUDCoreWebAPIEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                if (id < 1) { return BadRequest(); }

                var emp = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);

                if (emp == null) { return NotFound(); }

                return Ok(emp);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee emp)
        {
            try
            {
                _context.Add(emp);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Employee emp)
        {
            try
            {
                if (emp == null || emp.Id == 0) { return BadRequest(); }

                var empObj = await _context.Employees.FindAsync(emp.Id);

                if (empObj == null) { return NotFound(); }

                empObj.Name = emp.Name;
                empObj.Email = emp.Email;
                empObj.Address = emp.Address;
                empObj.HiredOn = emp.HiredOn;
                empObj.Position = emp.Position;
                empObj.ContactNo = emp.ContactNo;
                empObj.Dob = emp.Dob;
                empObj.DepartmentId = emp.DepartmentId;

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (id < 1) { return BadRequest(); }

                var emp = await _context.Employees.FindAsync(id);
                if (emp == null) { return NotFound(); }

                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
