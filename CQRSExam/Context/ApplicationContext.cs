using CQRSExam.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Context;

public class ApplicationContext:DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options){}
    
    public DbSet<Product> Products { get; set; }

    public new async Task<int> SaveChanges() => await base.SaveChangesAsync();
}