using BankingService.Functional;
using BankingService.Models.Contexts;
using BankingService.Models.DTOs;
using BankingService.Models.Entities;
using BankingService.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BankingService.Services
{
    public class StatementsService : IStatementsService
    {
        private readonly BankAccountsContext _context;
        private readonly IBankAccountService _accountService;

        public StatementsService(BankAccountsContext context, IBankAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<Result> CreateStatement(int accountID, Statement newStatement)
        {
            var result = await _accountService.GetAccount(accountID);

            if (!result.Success)
            {
                return Result.Fail<IEnumerable<Statement>>(result.StatusCode, result.Error);
            }

            if(result.Value.Statements?.Any(s => s.StatementID == newStatement.StatementID) == true)
            {
                return Result.Fail(HttpStatusCode.BadRequest, "Statement already exist");
            }

            try
            {
                var updatedAccount = result.Value;
                updatedAccount.Statements.Add(newStatement);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return Result.Fail(HttpStatusCode.InternalServerError, $"Unexpected server error while saving a new statement - {ex.Message}");
            }

            return Result.Ok();
        }

        public async Task<Result<IEnumerable<Statement>>> GetStatements(int accountID)
        {
            var result = await _accountService.GetAccount(accountID);

            if(!result.Success)
            {
                return Result.Fail<IEnumerable<Statement>>(result.StatusCode, result.Error);
            }

            return Result.Ok<IEnumerable<Statement>>(result.Value.Statements);
        }

        public async Task<Result<Statement>> GetStatement(int accountID, int statementID)
        {
            var result = await _accountService.GetAccount(accountID);

            if (!result.Success)
            {
                return Result.Fail<Statement>(result.StatusCode, result.Error);
            }

            var statement = result.Value.Statements?.FirstOrDefault(s => s.StatementID == statementID);

            if(statement == null)
            {
                return Result.Fail<Statement>(HttpStatusCode.NotFound, "Statement with specified ID was not found");
            }

            return Result.Ok(statement);
        }

        public async Task<Result> UpdateStatement(int accountID, int statementID, EditStatementDto updatedStatement)
        {
            try
            {
                var statement = await _context.Statements.FindAsync(statementID);

                if(statement == null)
                {
                    Result.Fail(HttpStatusCode.BadRequest, "Statement with specified ID could not be found");
                }

                statement.OperationType = updatedStatement.OperationType;
                statement.Amount = updatedStatement.Amount;

                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return Result.Fail(HttpStatusCode.InternalServerError, $"Unexpected server error while updating a statement - {ex.Message}");
            }

            return Result.Ok();
        }

        public async Task<Result> DeleteStatement(int accountID, int statementID)
        {
            var result = await GetStatement(accountID, statementID);

            if (!result.Success)
            {
                return Result.Fail<Statement>(result.StatusCode, result.Error);
            }

            _context.Statements.Remove(result.Value);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return Result.Fail(HttpStatusCode.InternalServerError, "Unexpected server error while deleting a statement");
            }

            return Result.Ok();
        }

    }
}
