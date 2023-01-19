using Microsoft.EntityFrameworkCore;

namespace Employee.Model.Context
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options){}

        public DbSet<HR_Employee> HR_Employees { get; set; }
        public DbSet<C_Org> C_Org { get; set; }
        public DbSet<HR_WorkingHistory> HR_WorkingHistorys { get; set; }

    }
}
