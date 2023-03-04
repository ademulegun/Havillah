using Havillah.ApplicationServices.Expense.Dto;
using Havillah.ApplicationServices.Interfaces;
using Havillah.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havillah.ApplicationServices.Expense.UseCases.Queries
{
    public class GetExpensesUseCaseQuery : IRequest<Result<List<GetExpenseDto>>>
    {
        public class GetExpensesUseCaseQueryHandler : IRequestHandler<GetExpensesUseCaseQuery, Result<List<GetExpenseDto>>>
        {
            private readonly IRepository<Core.Domain.Expense> _repository;
            public GetExpensesUseCaseQueryHandler(IRepository<Core.Domain.Expense> repository)
            {
                _repository = repository;
            }

            public async Task<Result<List<GetExpenseDto>>> Handle(GetExpensesUseCaseQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var expenses = await _repository.GetAll();
                    if (!expenses.Any()) return Result.Ok<List<GetExpenseDto>>(new List<GetExpenseDto>());
                    List<GetExpenseDto> expenseList = expenses.Select(v => new GetExpenseDto()
                    {
                        Title = v.Title,
                        Expenditure = v.Expenditure,
                        ExpenditureDate = v.ExpenditureDate,
                        ContractedBy = v.ContractedBy
                    }).ToList();
                    return Result.Ok(expenseList);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Result.Fail<List<GetExpenseDto>>("An error occured");
                }
            }
        }
    }
}
