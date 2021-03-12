using BankingService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingService.Models.DTOs
{
    public class EditBankAccountDto
    {
        public string AccountName { get; set; }
        public IList<Statement> Statements { get; set; }
    }
}
