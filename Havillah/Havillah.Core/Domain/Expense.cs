using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havillah.Core.Domain
{
    public class Expense: BaseEntity<Guid>
    {
        protected Expense() { }
        public string Title { get; set; }
        public List<Entity> Entities { get;set; } 
        public decimal Expenditure { get;set; }
        public DateTime ExpenditureDate { get;set; }
        public string ContractedBy { get; set; }
        public string Description { get; private set; }

        public static class ExpenseFactory
        {
            public static Expense Create(Guid id, string Title, decimal Expenditure, DateTime ExpenditureDate,
                string ContractedBy, string Description)
            {
                return new Product(id, Title, Expenditure, ExpenditureDate, productImageUrl, unitOfMeasureId);
            }
        }
    }
}
