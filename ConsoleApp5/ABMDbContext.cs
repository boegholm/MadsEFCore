
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
namespace KeramikTracker.Shared.DatabaseModels;

public class ABMDbContext : DbContext
{
    class OrderChange
    {

    }

    public ABMDbContext() : this(LoggerFactory.Create(b =>
    {
        b.ClearProviders();
    }))
    { }
    public ABMDbContext(ILoggerFactory loggerFactory) : base() { this.LF = loggerFactory; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(LF);
        //optionsBuilder.UseLoggerFactory(LoggerFactory.Create(b => {
        //    b.AddConsole();
        //}));
        optionsBuilder.UseSqlite("Data Source=db.sqlite");
        //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=db;Trusted_Connection=True;MultipleActiveResultSets=true");
        //optionsBuilder.EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder model)
    {
        model.ApplyConfigurationsFromAssembly(typeof(ABMDbContext).Assembly);
        base.OnModelCreating(model);
    }

    public override int SaveChanges()
    {
        var modorders = ChangeTracker.Entries<Order>().Where(v => v.State == EntityState.Modified);
        
        return base.SaveChanges();
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Table> Tables { get; set; }
    public ILoggerFactory LF { get; }
}
