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
    public class GetExpensesUseCaseQuery: IRequest<Result<List<GetExpenseDto>>>
    {
        public class GetExpensesUseCaseQueryHandler: IRequestHandler<GetExpensesUseCaseQuery, Result<List<GetExpenseDto>>>
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
                    List<GetExpenseDto> listOfExpenses = new List<GetExpenseDto>();
                    var expenses = await _repository.GetAll();
                    var companyexpenses = expenses as Core.Domain.Expense[] ?? expenses.ToArray();
                    if (!companyexpenses.Any()) return Result.Fail<List<GetExpenseDto>>("no available expenses found");
                    foreach (var exp in companyexpenses)
                    {
                        listOfExpenses.Add(new GetExpenseDto
                        {
                            Title = exp.Title,
                            Expenditure = exp.Expenditure,
                            ExpenditureDate = exp.ExpenditureDate,
                            ContractedBy = exp.ContractedBy
                        });
                    }
                    return Result.Ok(listOfExpenses);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Result.Fail<List<GetExpenseDto>>(e.Message);
                }
            }
        }
    }
}
