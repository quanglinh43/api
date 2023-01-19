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
            var result = _context.C_Org.ToList();
            List<C_Org1> list= new List<C_Org1>();
            foreach (var item in result) {
                list.Add(new C_Org1(item));
            }
            foreach (var lv1 in list)
            {
                lv1.C_Orgs = list.Where(p=>p.Parent_Id==lv1.C_Org_Id).ToList();
                foreach (var lv2 in lv1.C_Orgs)
                {
                    lv2.C_Orgs = list.Where(p => p.Parent_Id == lv2.C_Org_Id).ToList();
                    foreach (var lv3 in lv2.C_Orgs)
                    {
                        lv3.C_Orgs = list.Where(p => p.Parent_Id == lv3.C_Org_Id).ToList();
                    }

                }

            }
            return Ok(list.Where(p => p.Parent_Id == 0));

        }


    }
}
