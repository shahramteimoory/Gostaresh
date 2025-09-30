using MachineReporting.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MachineReporting.Api.Models.DataBaseContext
{
    public class DataBaseContext(DbContextOptions<DataBaseContext> options) : DbContext(options)
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
       public DbSet<Codes> Codes{ get; set; }
    }

}