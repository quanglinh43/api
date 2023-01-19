using Microsoft.AspNetCore.Mvc;

namespace Employee.Model.Context
{
    public class EmployeePage
    {
        public int HR_Employee_Id { get; set; }
        public DateTime Created_Date { get; set; }
        public string Created_User { get; set; }
        public DateTime Updated_Date { get; set; }
        public string Updated_User { get; set; }
        public Boolean IsActive { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        public string Mobiphone { get; set; }
        public int C_Org_Id { get; set; }
        public string Address { get; set; }
        public string UserLogin { get; set; }
        int TotalPage { get; set; }
        int PageIndex { get; set; }

    }
}
