using Havillah.ApplicationServices.Expense.Dto;
using Havillah.ApplicationServices.Interfaces;
using Havillah.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havillah.ApplicationServices.Expense.UseCases.Commands
{
    public class AddExpenseCommand : IRequest<Result>
    {
        public AddExpenseDto AddExpenseDto { get; set; }
        public class AddExpenseCommandHandler : IRequestHandler<AddExpenseCommand, Result>
        {
            private readonly IRepository<Core.Domain.Expense> _repository;
            public AddExpenseCommandHandler(IRepository<Core.Domain.Expense> repository)
            {
                _repository = _repository;
            }
            public async Task<Result> Handle(AddExpenseCommand request, CancellationToken cancellationToken)
            {
                var expenseId = Guid.NewGuid();
                var expense = Core.Domain.Expense.ExpenseFactory.Create(expenseId, request.AddExpenseDto.Title, request.AddExpenseDto.Expenditure,
                    request.AddExpenseDto.ExpenditureDate, request.AddExpenseDto.ContractedBy, request.AddExpenseDto.Description).SetTitle(request
                    .AddExpenseDto.Title).SetExpenditure(request.AddExpenseDto.Expenditure).SetDescription(request.AddExpenseDto.Description);
                await _repository.Add(model: expense);
                var expenseAdded = await _repository.Save();
                return expenseAdded > 0 ? Result.Ok("Expense added successfully", "00") : Result.Fail("Unable to add Expense", "01");
            }
        }
    }
}
