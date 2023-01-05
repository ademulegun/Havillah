using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havillah.Core.Domain
{
    public class Entity: BaseEntity<Guid>
    {
        public Entity() { }
        public Guid ExpenseId { get; set; }
        public Expense Expense { get; set; }
        public decimal AmountSpent { get; set; }
        public DateTime DateEstablished { get; set; }
        public string ContractBy { get;set; }
    }
}
