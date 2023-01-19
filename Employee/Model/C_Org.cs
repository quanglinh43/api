using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Model
{
    [Table("C_Org")]
    public class C_Org
    {
        [Key]
        public int C_Org_Id { get; set; }
        public DateTime Created_Date { get; set; }
        public string Created_User { get; set; }
        public DateTime Updated_Date { get; set; }
        public string Updated_User { get; set; }
        public Boolean IsActive { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int OrderValue { get; set; }
        public int Parent_Id { get; set; }
        
        

    }

    public class C_Org1
    {
        public int C_Org_Id { get; set; }
        public DateTime Created_Date { get; set; }
        public string Created_User { get; set; }
        public DateTime Updated_Date { get; set; }
        public string Updated_User { get; set; }
        public Boolean IsActive { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int OrderValue { get; set; }
        public int Parent_Id { get; set; }
        public List<C_Org1> C_Orgs { get; set; }
        public C_Org1(C_Org c)
        {
            this.C_Org_Id= c.C_Org_Id;
            this.Created_Date= c.Created_Date;
            this.Created_User= c.Created_User;
            this.Updated_Date= c.Updated_Date;
            this.Updated_User= c.Updated_User;
            this.IsActive= c.IsActive;
            this.Code= c.Code;
            this.Name= c.Name;
            this.OrderValue= c.OrderValue;
            this.Parent_Id= c.Parent_Id;

        }
    }
}
