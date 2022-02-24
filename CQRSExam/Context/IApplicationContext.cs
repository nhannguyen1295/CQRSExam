using CQRSExam.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Context;

public interface IApplicationContext
{
    Task<int> SaveChanges();
}