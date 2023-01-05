using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.User.Dto;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;
namespace Havillah.ApplicationServices.User.UseCases.Commands;
<<<<<<< HEAD
public class DeleteUserUseCaseCommand : IRequest<Result>
=======
<<<<<<< HEAD

<<<<<<< HEAD
public class DeleteUserUseCaseCommand: IRequest<Result>
>>>>>>> 2ac5f8b (rebase done and dusted)
{
    public Guid Id { get; set; }

    public class DeleteUserUseCaseCommandHandler : IRequestHandler<DeleteUserUseCaseCommand, Result>
    {
        private readonly IRepository<ApplicationUser> _repository;
        public DeleteUserUseCaseCommandHandler(IRepository<ApplicationUser> repository)
<<<<<<< HEAD
=======
=======
public class EditUserUseCaseCommand: IRequest<Result>
=======
public class DeleteUserUseCaseCommand: IRequest<Result>
>>>>>>> e34493e (modified espense with constructor)
{
    public Guid Id { get; set; }

    public class DeleteUserUseCaseCommandHandler: IRequestHandler<DeleteUserUseCaseCommand, Result>
    {
        private readonly IRepository<ApplicationUser> _repository;
<<<<<<< HEAD
        public EditUserUseCaseCommandHandler(IRepository<ApplicationUser> repository)
>>>>>>> 56eb5a1 (trying)
>>>>>>> 2ac5f8b (rebase done and dusted)
        {
            _repository = repository;
        }
        public async Task<Result> Handle(DeleteUserUseCaseCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Find(predicate: x => x.Id == request.Id);
            if (string.IsNullOrEmpty(user.Email)) return Result.Fail<GetUserDto>("User does not exist");
            _repository.Delete(user);
            var iseUserDeleted = await _repository.Save();
            return iseUserDeleted < 1 ? Result.Fail("Unable to delete user") : Result.Ok("Successfully deleted user");
<<<<<<< HEAD
=======
=======
        public async Task<Result> Handle(EditUserUseCaseCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Find(predicate: x => x.Id == request.Id);
            if(string.IsNullOrEmpty(user.Email)) return Result.Fail<GetUserDto>("User does not exist");
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.MiddleName = request.MiddleName;
            user.LastName = request.LastName;
            var iseUseUpdated = await _repository.Save();
            return iseUseUpdated < 1 ? Result.Fail("Unable to update user") : Result.Ok("Successfully updated user");
>>>>>>> 56eb5a1 (trying)
=======
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
>>>>>>> e34493e (modified espense with constructor)
>>>>>>> 2ac5f8b (rebase done and dusted)
        }
    }
}