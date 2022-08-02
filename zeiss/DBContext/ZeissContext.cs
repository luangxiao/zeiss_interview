using Microsoft.EntityFrameworkCore;
using zeiss.Models;

namespace zeiss.DBContext
{
    public class ZeissContext: DbContext
    {
        public DbSet<Socket> Sockets { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public ZeissContext(){}

        public ZeissContext(DbContextOptions<ZeissContext> options):base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Socket>().ToTable("Socket");
            modelBuilder.Entity<Machine>().ToTable("Machine");
        }
    }
}
