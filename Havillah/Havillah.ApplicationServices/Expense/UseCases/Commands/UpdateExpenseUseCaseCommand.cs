﻿using Havillah.ApplicationServices.Expense.Dto;
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
    public class UpdateExpenseUseCaseCommand : IRequest<Result>
    {
        public UpdateExpenseDto ExpenseDto { get; set; }
    }

    public class UpdateExpenseUseCaseCommandHandler : IRequestHandler<UpdateExpenseUseCaseCommand, Result>
    {
        private readonly IRepository<Core.Domain.Expense> _repository;
        public UpdateExpenseUseCaseCommandHandler(IRepository<Core.Domain.Expense> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateExpenseUseCaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var expenseFromDb = await _repository.Find(e => e.Id == request.ExpenseDto.Id);
                if (expenseFromDb.Id == default) return Result.Ok<UpdateExpenseDto>(new UpdateExpenseDto());
                expenseFromDb.SetDescription(request.ExpenseDto.Description).SetExpenditure(request.ExpenseDto.Expenditure)
                .SetTitle(request.ExpenseDto.Title);
                request.ExpenseDto.ExpenditureDate = expenseFromDb.ExpenditureDate;
                request.ExpenseDto.ContractedBy = expenseFromDb.ContractedBy;
                _repository.Update(model: expenseFromDb);
                var isUpdated = await _repository.Save();
                return isUpdated > 0 ? Result.Ok("Successfully updated expense", "00") : Result.Fail("unable to update expense", "01");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result.Fail("Something went wrong", "01");
            }

        }
    }
}
