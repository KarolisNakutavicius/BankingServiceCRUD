using BankingService.Enums;
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
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>().Property(ba => ba.ClientID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Statement>().Property(ba => ba.StatementID).ValueGeneratedOnAdd();


            modelBuilder.Entity<Statement>()
                .HasOne(s => s.BankAccount)
                .WithMany(b => b.Statements);
            //modelBuilder.Entity<BankAccount>()
            //    .HasMany(b => b.Statements)
            //    .WithOne();

            //_ = Task.Run(() => SeedData());
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Statement> Statements { get; set; }

        private void SeedData()
        {
            BankAccounts.AddRange(
                new BankAccount
                {
                    ClientID = 1,
                    AccountName = "UAB Vilniaus Transportas",
                    Statements = new List<Statement>
                    {
                        new Statement { Amount = 156, OperationType = OperationEnum.Expense, StatementID = 1},
                        new Statement { Amount = 230, OperationType = OperationEnum.Expense, StatementID = 2},
                    }
                },
                new BankAccount
                {
                    ClientID = 2,
                    AccountName = "MB Geras verslas",
                    Statements = new List<Statement>
                    {
                        new Statement { Amount = 5000, OperationType = OperationEnum.Income, StatementID = 3},
                        new Statement { Amount = 3000, OperationType = OperationEnum.Expense, StatementID = 4},
                    }
                });

            SaveChanges();
        }

    }
}
