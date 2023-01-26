using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Model
{
    [Table("HR_WorkingHistory")]
    public class HR_WorkingHistory
    {
        [Key]
        public Int64 HR_WorkingHistory_Id { get; set; }
        public DateTime Created_Date { get; set; }
        public string Created_User { get; set; }
        public DateTime Updated_Date { get; set; }
        public string Updated_User { get; set; }
        public Boolean IsActive { get; set; }
        public int HR_Employee_Id { get; set; }
        public int C_Org_Id { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public string JobTitle { get; set; }
        public decimal SalaryAmt { get; set; }
    }
}
