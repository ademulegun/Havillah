using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.User.Dto;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.User.UseCases.Queries;
public class GetUsersUseCaseQuery : IRequest<Result<List<GetUserDto>>>
{

    public class GetUsersUseCaseQueryHandler : IRequestHandler<GetUsersUseCaseQuery, Result<List<GetUserDto>>>
    {
        private readonly IRepository<ApplicationUser> _repository;
        public GetUsersUseCaseQueryHandler(IRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }
        public async Task<Result<List<GetUserDto>>> Handle(GetUsersUseCaseQuery request, CancellationToken cancellationToken)
        {
            List<GetUserDto> listOUsers = new List<GetUserDto>();
            var users = await _repository.GetAll();
            var applicationUsers = users as ApplicationUser[] ?? users.ToArray();
            if (!applicationUsers.Any()) return Result.Fail<List<GetUserDto>>("No user found");
            foreach (var user in applicationUsers)
            {
                listOUsers.Add(new GetUserDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName
                });
            }
            return Result.Ok(listOUsers);

        }
    }
}