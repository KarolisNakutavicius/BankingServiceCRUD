using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingService.Models.Contexts;
using BankingService.Models.Entities;
using BankingService.Services.Contracts;

namespace BankingService.Controllers
{
    [Route("BankAccounts/{accountID}/[controller]")]
    [ApiController]
    public class StatementsController : ControllerBase
    {
        private readonly IStatementsService _statementsService;

        public StatementsController(IStatementsService statementsService)
        {
            _statementsService = statementsService;
        }

        [HttpPost]
        public async Task<ActionResult<Statement>> CreateStatement(int accountID, Statement newStatement)
        {
            var result = await _statementsService.CreateStatement(accountID, newStatement);

            if (!result.Success)
            {
                return new JsonResult(result) { StatusCode = (int)result.StatusCode };
            }

            return CreatedAtAction("GetStatement", new { id = newStatement.StatementID }, newStatement);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Statement>>> GetStatements(int accountID)
        {
            var result = await _statementsService.GetStatements(accountID);

            if (!result.Success)
            {
                return new JsonResult(result) { StatusCode = (int)result.StatusCode };
            }

            return Ok(result.Value);
        }

        [HttpGet("{statementID}")]
        public async Task<ActionResult<Statement>> GetStatement(int accountID, int statementID)
        {
            var result = await _statementsService.GetStatement(accountID, statementID);

            if (!result.Success)
            {
                return new JsonResult(result) { StatusCode = (int)result.StatusCode };
            }

            return Ok(result.Value);
        }

        [HttpPut("{statementID}")]
        public async Task<IActionResult> PutStatement(int accountID, int statementID, Statement updatedStatement)
        {
            var result = await _statementsService.UpdateStatement(accountID, statementID, updatedStatement);

            if (!result.Success)
            {
                return new JsonResult(result) { StatusCode = (int)result.StatusCode };
            }

            return NoContent();           
        }

        [HttpDelete("{statementID}")]
        public async Task<IActionResult> DeleteStatement(int accountID, int statementID)
        {
            var result = await _statementsService.DeleteStatement(accountID, statementID);

            if (!result.Success)
            {
                return new JsonResult(result) { StatusCode = (int)result.StatusCode };
            }

            return NoContent();
        }
    }
}
