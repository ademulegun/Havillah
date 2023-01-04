using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.User.Dto;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.User.UseCases.Commands;

public class DeleteUserUseCaseCommand: IRequest<Result>
{
    public Guid Id { get; set; }

    public class DeleteUserUseCaseCommandHandler: IRequestHandler<DeleteUserUseCaseCommand, Result>
    {
        private readonly IRepository<ApplicationUser> _repository;
        public DeleteUserUseCaseCommandHandler(IRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }
        
        public async Task<Result> Handle(DeleteUserUseCaseCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Find(predicate: x => x.Id == request.Id);
            if(string.IsNullOrEmpty(user.Email)) return Result.Fail<GetUserDto>("User does not exist");
            _repository.Delete(user);
            var iseUserDeleted = await _repository.Save();
            return iseUserDeleted < 1 ? Result.Fail("Unable to delete user") : Result.Ok("Successfully deleted user");
        }
    }   
}