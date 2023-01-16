using Havillah.ApplicationServices.Expense.Dto;
using Havillah.ApplicationServices.Interfaces;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havillah.ApplicationServices.Expense.UseCases.Queries
{
    public class GetExpensesByDateUseCaseQuery: IRequest<Result<GetExpenseDto>>
    {
        public DateTime ExpenditureDate { get; set; }
        public class GetExpensesByEmailUseCaseQueryHandler: IRequestHandler<GetExpensesByDateUseCaseQuery, Result<GetExpenseDto>>
        {
            private readonly IRepository<Core.Domain.Expense> _repository;
            public GetExpensesByEmailUseCaseQueryHandler(IRepository<Core.Domain.Expense> repository)
            {
                _repository = repository;
            }

            public async Task<Result<GetExpenseDto>> Handle(GetExpensesByDateUseCaseQuery request, CancellationToken cancellationToken)
            {
                var exp = await _repository.Find(predicate: x => x.ExpenditureDate == request.ExpenditureDate);
                if (string.IsNullOrEmpty(exp.Title)) return Result.Fail<GetExpenseDto>("Nothing found that matches the request");
                return Result.Ok(new GetExpenseDto
                {
                    Title = exp.Title,
                    Expenditure = exp.Expenditure,
                    ExpenditureDate = exp.ExpenditureDate,
                    ContractedBy = exp.ContractedBy
                });
            }
        }
    }
}
