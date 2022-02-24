using CQRSExam.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Context;

public interface IApplicationContext
{
    DbSet<Product> Products { get; set; }
    new Task<int> SaveChanges();
}