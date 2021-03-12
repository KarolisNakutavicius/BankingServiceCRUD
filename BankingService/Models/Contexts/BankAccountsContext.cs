using BankingService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingService.Models.Contexts
{
    public class BankAccountsContext : DbContext
    {
        public BankAccountsContext(DbContextOptions<BankAccountsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>().Property(ba => ba.ClientID).ValueGeneratedNever();
            modelBuilder.Entity<Statement>().Property(ba => ba.StatementID).ValueGeneratedNever();

            modelBuilder.Entity<BankAccount>()
                .HasMany(b => b.Statements)
                .WithOne();
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Statement> Statements { get; set; }
    }
}
