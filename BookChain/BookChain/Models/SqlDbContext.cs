using System.Data.Entity;

namespace BookChain.Models
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() : base("name=SqlConn") // Read from webconfig
        {
        }
        public DbSet<Employee> Employees { get; set; }

       
    }
}