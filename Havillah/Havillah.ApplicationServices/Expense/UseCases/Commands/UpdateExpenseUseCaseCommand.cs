using Havillah.ApplicationServices.Expense.Dto;
using Havillah.ApplicationServices.Expense.ViewModels;
using Havillah.ApplicationServices.Interfaces;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havillah.ApplicationServices.Expense.UseCases.Commands
{
    public class UpdateExpenseUseCaseCommand: IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
       // public List<Entity> Entities { get; set; }
        public decimal Expenditure { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public string ContractedBy { get; set; }
        public string Description { get; private set; }
        public UpdateExpenseDto UpdateExpenseDto { get; set; }
    }

    public class UpdateExpenseUseCaseCommandHandler: IRequestHandler<UpdateExpenseUseCaseCommand, Result>
    {
        //public UpdateExpense UpdateExpense { get; set; }
        private readonly IRepository<Core.Domain.Expense> _repository;
        public UpdateExpenseUseCaseCommandHandler(IRepository<Core.Domain.Expense> repository)
        {
            _repository = repository;   
        }

        public async Task<Result> Handle(UpdateExpenseUseCaseCommand request, CancellationToken cancellationToken)
        {
            var expense = await _repository.Find(predicate: e => e.Id == request.Id);
            if (string.IsNullOrEmpty(expense.Title)) return Result.Fail("such expense does not exist");
            expense.Title = request.Title;
            expense.Expenditure = request.Expenditure;
            expense.ExpenditureDate = request.ExpenditureDate;  
            expense.ContractedBy = request.ContractedBy;
            //expense.Description = request.Description;

            _repository.Update(expense);
            var isUpdated = await _repository.Save();
            return isUpdated < 1 ? Result.Fail("Unable to update expense") : Result.Ok("Successfully updated expense");


        }
    }
    
}
