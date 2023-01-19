using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee.Model;
using Employee.Model.Context;
using Microsoft.AspNetCore.Cors;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HR_EmployeeController : ControllerBase
    {
        private readonly Context _context;

        public HR_EmployeeController(Context context)
        {
            _context = context;
        }

        // GET: api/HR_Employee
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<HR_Employee>>> GetHR_Employees()
        //{
        //    return await _context.HR_Employees.ToListAsync();
        //}
        [HttpGet]
        public async Task<ActionResult> GetHR_Employees()
        {
            var result = from e in _context.HR_Employees
                         join o in _context.C_Org on e.C_Org_Id equals o.C_Org_Id
                         select new
                         {
                             HR_Employee = e,
                             C_Org_name = o.Name
                         };

            return Ok(result);
        }
        //Get:Paging
        [HttpGet("{page},{size}")]
        public async Task<IActionResult> GetHR_EmployeesPagging(int page = 1, int size = 10)
        {
            var all = ( from e in _context.HR_Employees
                      join o in _context.C_Org on e.C_Org_Id equals o.C_Org_Id
                      select new
                      {
                        e,
                        C_Org_name = o.Name
                      });
            List<Infor> infors= new List<Infor>();
            foreach(var item in all)
            {
                infors.Add(new Infor(item.e, item.C_Org_name));
            }
            var totalCount = all.Count();
            var result = new PageInfo<Infor>()
            {
                Items = infors.Skip((page - 1) * size).Take(size).ToList(),
                PageIndex = page,
                TotalPage = (int)Math.Ceiling(totalCount / (Double)size),

            };
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<HR_Employee>> GetHR_Employee(int id)
        {
            var hR_Employee = await _context.HR_Employees.FindAsync(id);

            if (hR_Employee == null)
            {
                return NotFound();
            }

            return hR_Employee;
        }

        // PUT: api/HR_Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHR_Employee(int id, HR_Employee hR_Employee)
        {
            if (id != hR_Employee.HR_Employee_Id)
            {
                return BadRequest();
            }

            _context.Entry(hR_Employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HR_EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateActive(int id)
        {
            var employee = _context.HR_Employees.Find(id);
            if(employee==null)
            {
                return NotFound();
            }    
            employee.IsActive = !employee.IsActive;
            _context.SaveChanges();
            return NoContent();
        }

        // POST: api/HR_Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HR_Employee>> PostHR_Employee(HR_Employee hR_Employee)
        {
            _context.HR_Employees.Add(hR_Employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHR_Employee", new { id = hR_Employee.HR_Employee_Id }, hR_Employee);
        }

        // DELETE: api/HR_Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHR_Employee(int id)
        {
            var hR_Employee = await _context.HR_Employees.FindAsync(id);
            if (hR_Employee == null)
            {
                return NotFound();
            }

            _context.HR_Employees.Remove(hR_Employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HR_EmployeeExists(int id)
        {
            return _context.HR_Employees.Any(e => e.HR_Employee_Id == id);
        }
    }
}
