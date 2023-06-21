
using DatingApp.Entities;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;

namespace DatingApp.Data
{
    public class DataContext : DbContext
    {

        public DbSet<AppUser> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

      
    }
}
