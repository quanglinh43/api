using Employee.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            List<Customers> customers = new List<Customers>();
            customers.Add(new Customers()
            {
                CustomerID = "1",
                CustomerFirstName = "Linh",
                CustomerLastName = "Vũ"
            });
            customers.Add(new Customers()
            {
                CustomerID = "2",
                CustomerFirstName = "Hong",
                CustomerLastName = "Nguyen"
            });
            customers.Add(new Customers()
            {
                CustomerID = "3",
                CustomerFirstName = "huong",
                CustomerLastName = "tran"
            });
            customers.Add(new Customers()
            {
                CustomerID = "4",
                CustomerFirstName = "nhung",
                CustomerLastName = "dang"
            });
            customers.Add(new Customers()
            {
                CustomerID = "5",
                CustomerFirstName = "thang",
                CustomerLastName = "nguyen"
            });
            return Ok(customers);

        }
    }
}
