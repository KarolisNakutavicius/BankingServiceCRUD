using BankingService.Functional;
using BankingService.Models.Contexts;
using BankingService.Models.Entities;
using BankingService.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BankingService.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BankAccountsContext _context;
        public BankAccountService(BankAccountsContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<BankAccount>>> GetAccounts()
        {
            return await _context.BankAccounts.Include(ba => ba.Statements).ToListAsync();
        }

        public async Task<Result> CreateAccount(BankAccount newAccount)
        {
            if(newAccount.ClientID < 1)
            {
                return Result.Fail(HttpStatusCode.BadRequest, "ID cannot be 0 or lower");
            }
            if (BankAccountExists(newAccount.ClientID))
            {
                return Result.Fail(HttpStatusCode.BadRequest, "Account already exists");
            }

            _context.BankAccounts.Add(newAccount);
            //_context.BankAccounts.Wh
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Result.Fail(HttpStatusCode.InternalServerError, "Unexpected server error while saving new account");
            }

            return Result.Ok();

        }

        public async Task<Result<BankAccount>> GetAccount(int id)
        {
            BankAccount bankAccount = null;

            try
            {
                bankAccount = await _context.BankAccounts.Include(ba => ba.Statements).FirstOrDefaultAsync(ba => ba.ClientID == id);
            }
            catch
            {
                //account was not found 
            }

            if (bankAccount == null)
            {
                return Result.Fail<BankAccount>(HttpStatusCode.NotFound, "Bank account was not found");
            }

            return Result.Ok(bankAccount);
        }

        public async Task<Result> UpdateAccount(int id, BankAccount updatedAccount)
        {
            if (id != updatedAccount.ClientID)
            {
                return Result.Fail(HttpStatusCode.BadRequest, "Account IDs does not match");
            }

            try
            {
                _context.Entry(updatedAccount).State = EntityState.Modified;

                foreach (var statment in updatedAccount.Statements)
                {
                    _context.Entry(statment).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankAccountExists(id))
                {
                    return Result.Fail(HttpStatusCode.BadRequest, "Account was not found");
                }
                else
                {
                    return Result.Fail(HttpStatusCode.InternalServerError, "Unexpected server error while updating an account");
                }
            }

            return Result.Ok();
        }

        public async Task<Result> DeleteAccount(int id)
        {
            var result = await GetAccount(id);

            if(!result.Success)
            {
                return result;
            }    

            try
            {
                _context.BankAccounts.Remove(result.Value);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return Result.Fail(HttpStatusCode.InternalServerError, "Unexpected server error while deleting an account");
            }

            return Result.Ok();
        }


        private bool BankAccountExists(int id)
        {
            return _context.BankAccounts.Any(e => e.ClientID == id);
        }
    }
}
