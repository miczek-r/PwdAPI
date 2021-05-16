using Microsoft.EntityFrameworkCore;

namespace ZarzadzanieDomem.Models.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<TypeOfExpense> TypesOfExpenses { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasMany<Expense>().WithOne().HasForeignKey(x => x.OwnerId);
            modelBuilder.Entity<Home>()
                .HasMany<User>().WithOne().HasForeignKey(x => x.HomeId);
            modelBuilder.Entity<TypeOfExpense>()
                .HasMany<Expense>().WithOne().HasForeignKey(x => x.TypeOfExpenseId);
            modelBuilder.Entity<User>()
                .HasMany<Notification>().WithOne().HasForeignKey(x => x.ReceiverEmail).HasPrincipalKey(x => x.Email);
        }
    }
}
