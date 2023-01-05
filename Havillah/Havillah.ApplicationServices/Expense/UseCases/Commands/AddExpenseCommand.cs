using Havillah.ApplicationServices.Expense.Dto;
using Havillah.ApplicationServices.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havillah.ApplicationServices.Expense.UseCases.Commands
{
    public class AddExpenseCommand: IRequest<string>
    {
        public AddExpenseDto AddExpenseDto { get; set; }
        public class AddExpenseCommandHandler: IRequestHandler<AddExpenseCommand, string>
        {
            private readonly IRepository<Core.Domain.Expense> _repository;
            public AddExpenseCommandHandler(IRepository<Core.Domain.Expense> repository)
            {
                _repository = _repository;
            }
            public async Task<string> Handle(AddExpenseCommand request, CancellationToken cancellationToken)
            {
                var expenseId = Guid.NewGuid();
                var expense = Core.Domain.Expense.ExpenseFactory.Create(expenseId, request.AddExpenseDto.Title, request.AddExpenseDto.Expenditure,
                    request.AddExpenseDto.ExpenditureDate, request.AddExpenseDto.ContractedBy, request.AddExpenseDto.Description);
                await _repository.Add(expense);
                return await _repository.Save() > 1 ? "Product added successfully" : "Unable to add product";
            }
        }
    }
}
