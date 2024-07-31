using Demo.DAL.Entity;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class DemoContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Department> Department { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<City> City { get; set; }
    public DbSet<District> District { get; set; }

    public DemoContext(DbContextOptions<DemoContext> options) : base(options) { }

  

}
