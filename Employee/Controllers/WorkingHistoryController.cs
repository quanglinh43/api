using Employee.Model;
using Employee.Model.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingHistoryController : ControllerBase
    {
        private readonly Context _context;

        public WorkingHistoryController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetWHByIdEmployee(int id)
        {
            var result = _context.HR_WorkingHistorys.Where(p=>p.HR_Employee_Id==id).OrderByDescending(p1=>p1.From_Date);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWHById(int id)
        {
            var result = _context.HR_WorkingHistorys.Where(p=>p.HR_WorkingHistory_Id==id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreatewWH(HR_WorkingHistory wh)
        {
            try
            {
                var work = _context.HR_WorkingHistorys.Where(p=>p.HR_Employee_Id==wh.HR_Employee_Id).OrderBy(p => p.From_Date).LastOrDefault();
                if(work!=null)
                {
                    DateTime end = wh.From_Date.AddDays(-1);
                    work.To_Date = end;
                    _context.SaveChanges();
                }    
                
                wh.Created_Date = DateTime.Now;
                wh.Updated_Date = DateTime.Now;
                wh.Updated_User = "linh";
                wh.Created_User = "linh";
                _context.HR_WorkingHistorys.Add(wh);
                _context.SaveChanges();
                return Ok(wh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }
        [HttpPut("{id}")]
        public IActionResult UpdateWH(int id, HR_WorkingHistory wh)
        {
            
            
            DateTime dateTime= DateTime.Now;
            var _wh = _context.HR_WorkingHistorys.Where(p => p.HR_WorkingHistory_Id == id).FirstOrDefault();
            if (_wh==null)
            {
                return NotFound();

            }
            else
            {
                
                _wh.To_Date = wh.To_Date;
                _wh.From_Date= wh.From_Date;
                _wh.JobTitle= wh.JobTitle;
                _wh.Updated_Date = dateTime;
                _wh.C_Org_Id = wh.C_Org_Id;
                _wh.SalaryAmt = wh.SalaryAmt;
                _wh.Updated_User = "linh";
                _context.SaveChanges();
                return NoContent();
            }    
        }
        [HttpPut]
        public async Task<IActionResult> UpdateActive(int id)
        {
            var wh = _context.HR_WorkingHistorys.Where(p => p.HR_WorkingHistory_Id == id).FirstOrDefault();
            if (wh == null)
            {
                return NotFound();
            }
            wh.IsActive = !wh.IsActive;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteWH(int id)
        {
            var wh = _context.HR_WorkingHistorys.Where(p=>p.HR_WorkingHistory_Id==id).FirstOrDefault();
            if(wh==null)
            {
                return NotFound();
            }
            _context.HR_WorkingHistorys.Remove(wh);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
