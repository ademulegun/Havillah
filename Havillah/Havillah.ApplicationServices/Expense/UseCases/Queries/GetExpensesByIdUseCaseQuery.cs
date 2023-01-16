﻿using Havillah.ApplicationServices.Expense.Dto;
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
    public class GetExpensesByIdUseCaseQuery: IRequest<Result<GetExpenseDto>>
    {
        public Guid Id { get; set; }
        public class GetExpensesUseCaseQueryHandler: IRequestHandler<GetExpensesByIdUseCaseQuery, Result<GetExpenseDto>>
        {
            private readonly IRepository<Core.Domain.Expense> _repository;
            public GetExpensesUseCaseQueryHandler(IRepository<Core.Domain.Expense> repository)
            {
                _repository = repository;
            }

            public async Task<Result<GetExpenseDto>> Handle(GetExpensesByIdUseCaseQuery request, CancellationToken cancellationToken)
            {
                var expense = await _repository.Find(predicate: x => x.Id == request.Id);
                if (string.IsNullOrEmpty(expense.Title)) return Result.Fail<GetExpenseDto>("no expenses found");
                return Result.Ok<GetExpenseDto>(new GetExpenseDto
                {
                    Title = expense.Title,
                    Expenditure = expense.Expenditure,
                    ExpenditureDate = expense.ExpenditureDate,
                    ContractedBy = expense.ContractedBy
                });
            }
        }

    }
}