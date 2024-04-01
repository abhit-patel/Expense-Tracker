using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace ExpenseTracker.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<TransactionClass> Transactions { get; set; }
        public DbSet<CategoryClass> Categories { get; set; }
    }
}
