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
        private Expense(Guid id, string title, decimal expenditure, DateTime expenditureDate, string contractedBy, string description)
        {
            Id = id;
            Title = title;
            Expenditure = expenditure;
            ExpenditureDate = expenditureDate;
            ContractedBy = contractedBy;
            Description = description;
        }

        public string Title { get; private set; }
        public List<Entity> Entities { get;set; } 
        public decimal Expenditure { get;private set; }
        public DateTime ExpenditureDate { get;set; }
        public string ContractedBy { get; set; }
        public string Description { get; private set; }

        public static class ExpenseFactory
        {
            public static Expense Create(Guid id, string title, decimal expenditure, DateTime expenditureDate,
                string contractedBy, string description)
            {
                return new Expense(id, title, expenditure, expenditureDate, contractedBy, description);
            }
        }
        public Expense SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description)) return this;
            this.Description = description;
            return this;
        }
        
        public Expense SetExpenditure(decimal expenditure)
        {
            if (expenditure <= 0) return this;
            this.Expenditure = expenditure;
            return this;
        }

        public Expense SetTitle(string title)
        {
            if(string.IsNullOrEmpty(title)) return this;
            this.Title = title;
            return this;
        }
    }
}
