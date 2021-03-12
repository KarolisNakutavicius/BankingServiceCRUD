using BankingService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingService.Models.DTOs
{
    public class EditStatementDto
    {
        public OperationEnum OperationType { get; set; }
        public float Amount { get; set; }
    }
}
