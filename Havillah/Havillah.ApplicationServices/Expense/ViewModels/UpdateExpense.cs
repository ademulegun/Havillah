﻿using Havillah.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havillah.ApplicationServices.Expense.ViewModels
{
    public class UpdateExpense
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        //public List<Entity> Entities { get; set; }
        public decimal Expenditure { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public string ContractedBy { get; set; }
        //public string Description { get; private set; }
    }
}