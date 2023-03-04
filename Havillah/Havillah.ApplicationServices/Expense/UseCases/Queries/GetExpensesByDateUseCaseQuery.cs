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
    public class GetExpensesByDateUseCaseQuery : IRequest<Result<GetExpenseDto>>
    {
        public DateTime ExpenditureDate { get; set; }
        public class GetExpensesByEmailUseCaseQueryHandler : IRequestHandler<GetExpensesByDateUseCaseQuery, Result<GetExpenseDto>>
        {
            private readonly IRepository<Core.Domain.Expense> _repository;
            public GetExpensesByEmailUseCaseQueryHandler(IRepository<Core.Domain.Expense> repository)
            {
                _repository = repository;
            }

            public async Task<Result<GetExpenseDto>> Handle(GetExpensesByDateUseCaseQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var exp = await _repository.Find(predicate: x => x.ExpenditureDate == request.ExpenditureDate);
                    if (exp.ExpenditureDate == default) return Result.Ok<GetExpenseDto>(new GetExpenseDto());
                    var expense = new GetExpenseDto()
                    {
                        Title = exp.Title,
                        Expenditure = exp.Expenditure,
                        ExpenditureDate = exp.ExpenditureDate,
                        ContractedBy = exp.ContractedBy
                    };
                    return Result.Ok(expense);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                    return Result.Fail<GetExpenseDto>("error somewhere", "01");
                }
            }
        }
    }
}
