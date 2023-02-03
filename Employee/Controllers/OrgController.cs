using Employee.Model;
using Employee.Model.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Packaging;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgController : ControllerBase
    {
        private readonly Context _context;

        public OrgController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult getMenu()
        {

            List<Menu> list = new List<Menu>();
            Menu[] mn =
            {
                new Menu(1, "Menu1", 0,1),
                new Menu(2, "Menu2", 0,2),
                new Menu(3, "Menu3", 0,3),
                new Menu(4, "Menu4", 0,4),
                new Menu(5, "Menu1.1", 1,5),
                new Menu(6, "Menu1.2", 1,6),
                new Menu(7, "Menu1.3", 1,7),
                new Menu(8, "Menu1.1.1", 5,8),
                new Menu(9, "Menu1.1.2", 5,9),
                new Menu(10, "Menu1.2.1", 6,10),
                new Menu(11, "Menu2.1", 2,11),
                new Menu(12, "Menu3.1", 3,12),
                new Menu(13, "Menu4.1", 4,13)
            };
            list.AddRange(mn);
            List<Menu> menus = list.OrderBy(m => m.Order).ToList();
            //Đệ quy
            //int count = 0;
            //string loadmenu(int parentid,int level)
            //{
            //    string result = string.Empty;
            //    if(menus.Count==0)
            //    {
            //        return result;
            //    }
            //    else
            //    {
            //        result += "";
            //        foreach(var m in menus.Where(p=>p.ParentId==parentid))
            //        {

            //            result += ""+m.Name+"/"+level+ ",";
            //            result += loadmenu(m.Id,level+1);
            //        }
            //        count++;
            //        result += "";

            //    }
            //    return result;
            //}
            //var results= loadmenu(0,0);
            //results = results.Remove(results.Length - 1);
            //var array = results.Split(",");

            //return Ok(array);

            //fix cứng
            foreach (var lv1 in menus)
            {
                lv1.Menus = menus.Where(p => p.ParentId == lv1.Id).ToList();
                foreach (var lv2 in lv1.Menus)
                {
                    lv2.Menus = menus.Where(p => p.ParentId == lv2.Id).ToList();
                    foreach (var lv3 in lv2.Menus)
                    {
                        lv3.Menus = menus.Where(p => p.ParentId == lv3.Id).ToList();
                    }

                }

            }

            return Ok(menus.Where(p => p.ParentId == 0));


        }

        [HttpGet("get")]
        public IActionResult getOrg()
        {
            var result = _context.C_Org.Where(p=>p.IsActive==true).OrderBy(p=>p.OrderValue).ToList();
            List<C_Org1> list= new List<C_Org1>();
            foreach (var item in result) {
                list.Add(new C_Org1(item));
            }
            //foreach (var lv1 in list)
            //{
            //    lv1.C_Orgs = list.Where(p=>p.Parent_Id==lv1.C_Org_Id).ToList();
            //    foreach (var lv2 in lv1.C_Orgs)
            //    {
            //        lv2.C_Orgs = list.Where(p => p.Parent_Id == lv2.C_Org_Id).ToList();
            //        foreach (var lv3 in lv2.C_Orgs)
            //        {
            //            lv3.C_Orgs = list.Where(p => p.Parent_Id == lv3.C_Org_Id).ToList();
            //        }

            //    }

            //}
            //return Ok(list.Where(p => p.Parent_Id == 0));

            List<C_Org1> loadOrg(int id, List<C_Org1> org)
            {
                foreach(var item in org)
                {
                    item.C_Orgs= list.Where(p=>p.Parent_Id==item.C_Org_Id).ToList();
                    if(item.C_Orgs.Count>0)
                    {
                        loadOrg(item.C_Org_Id, item.C_Orgs);
                    }    
                }
                return org.Where(p => p.Parent_Id == 0).ToList();
            }
            var result1 = loadOrg(0, list);
            return Ok(result1);

        }
        [HttpGet("getAll")]
        public IActionResult getAllOrg() 
        {
            var result = _context.C_Org.OrderBy(p=>p.Name).ToList();

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id) 
        {
            var result = await _context.C_Org.FindAsync(id);
            if(result==null)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrg(C_Org org)
        {
            org.C_Org_Id = 0;
            org.Created_Date= DateTime.Now;
            org.Created_User = "linh";
            org.Updated_Date = DateTime.Now;
            org.Updated_User = "linh";
            org.IsActive= true;
            try
            {
                await _context.C_Org.AddAsync(org);
                await _context.SaveChangesAsync();
                return Ok(org);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrg(int id, C_Org org)
        {
            var _org = await _context.C_Org.FindAsync(id);
            if (_org == null)
                return NotFound();
            _org.OrderValue = org.OrderValue;
            _org.Code = org.Code;
            _org.Name = org.Name;
            _org.Parent_Id = org.Parent_Id;
            _org.Updated_Date= DateTime.Now;
            _org.Updated_User = "linh";
            try
            {
                await _context.SaveChangesAsync();
                return Ok(_org);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut]
        public async Task<IActionResult> UpdateActive(int id)
        {
            var _org = _context.C_Org.Where(p => p.C_Org_Id == id).FirstOrDefault();
            if (_org == null)
            {
                return NotFound();
            }
            _org.IsActive = !_org.IsActive;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrg(int id)
        {
            
            var employees = _context.HR_Employees.Where(p => p.C_Org_Id == id).ToList();
            var childs= _context.C_Org.Where(p=>p.Parent_Id== id).ToList();
            if (childs.Count != 0)
            {
                haveChild child = new haveChild(childs.Count);
                return Ok(child);
            }

            if (employees.Count != 0)
            {
                haveEmployee employee = new haveEmployee(employees.Count);
                return Ok(employee);
            }
            
            var _org = await _context.C_Org.FindAsync(id);
            if (_org == null)
                return NotFound();
            try
            {
                _context.C_Org.Remove(_org);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        
        class haveEmployee
        {
            public string name { get; }
            public int number { get; set; }
            public haveEmployee(int _number)
            {
                name = "employee";
                number = _number;
            }
        }
        class haveChild
        {
            public string name { get; }
            public int number { get; set; }
            public haveChild(int _number)
            {
                name = "child";
                number = _number;
            }
        }
    }
}
