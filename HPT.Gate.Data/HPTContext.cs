using HPT.Gate.Data.Model;
using System.Data.Common;
using System.Data.Entity;

namespace HPT.Gate.Data
{
    public class HPTContext : DbContext
    {
        private static DbConnection Conn = null;
        public HPTContext() : base(Conn, true)
        {

        }

        public DbSet<Dept> Depts { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
