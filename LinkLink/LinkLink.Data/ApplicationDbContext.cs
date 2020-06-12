using LinkLink.Data.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkLink.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<EmployeeOffice> EmployeesOffices { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EmployeeOffice>()
                            .HasKey(eo => new { eo.EmployeeId, eo.OfficeId });

            builder.Entity<EmployeeOffice>()
                        .HasOne(eo => eo.Employee)
                        .WithMany(o => o.EmployeesOffices)
                        .HasForeignKey(bc => bc.EmployeeId);
            builder.Entity<EmployeeOffice>()
                         .HasOne(bc => bc.Office)
                         .WithMany(c => c.EmployeesOffices)
                         .HasForeignKey(bc => bc.OfficeId);
        }



    }
}
