using CJ.Exp.Data.EF.DataModels;
using CJ.Exp.ServiceModels.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CJ.Exp.Data.EF
{
  public class ExpDbContext : IdentityDbContext<ApplicationUserEf>
  {
    public ExpDbContext(DbContextOptions<ExpDbContext> options)
            : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      // Customize the ASP.NET Identity model and override the defaults if needed.
      // For example, you can rename the ASP.NET Identity table names and more.
      // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<ExpenseDM> Expenses { get; set; }
    public DbSet<ExpenseTypeDM> ExpenseTypes { get; set; }

  }
}
