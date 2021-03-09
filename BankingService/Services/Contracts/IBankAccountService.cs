using BankingService.Functional;
using BankingService.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingService.Services.Contracts
{
    public interface IBankAccountService
    {
        Task<Result> CreateAccount(BankAccount newAccount);
        Task<ActionResult<IEnumerable<BankAccount>>> GetAccounts();
        Task<Result<BankAccount>> GetAccount(int id);
        Task<Result> UpdateAccount(int id, BankAccount newAccount);
        Task<Result> DeleteAccount(int id);

    }
}
