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
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Statement> Statement { get; set; }
    }
}
