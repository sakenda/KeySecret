using KeySecret.DataAccess.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace KeySecret.DataAccess.Library.Context
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
        }

        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
    }
}