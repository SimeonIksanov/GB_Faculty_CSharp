using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context;

public class AppDb : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Report> Reports { get; set; }
    public AppDb(DbContextOptions options) : base(options)
    {
    }
}
