using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.User.Dto;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.User.UseCases.Queries;

<<<<<<< HEAD
public class GetUsersUseCaseQuery: IRequest<Result<List<GetUserDto>>>
{

    public class GetUsersUseCaseQueryHandler: IRequestHandler<GetUsersUseCaseQuery, Result<List<GetUserDto>>>
    {
        private readonly IRepository<ApplicationUser> _repository;
        public GetUsersUseCaseQueryHandler(IRepository<ApplicationUser> repository)
=======
public class GetUserByEmailUseCaseQuery: IRequest<Result<GetUserDto>>
{
    public string Email { get; set; } = null!;
    
    public class GetUserByEmailUseCaseQueryHandler: IRequestHandler<GetUserByEmailUseCaseQuery, Result<GetUserDto>>
    {
        private readonly IRepository<ApplicationUser> _repository;
        public GetUserByEmailUseCaseQueryHandler(IRepository<ApplicationUser> repository)
>>>>>>> 56eb5a1 (trying)
        {
            _repository = repository;
        }
        
<<<<<<< HEAD
        public async Task<Result<List<GetUserDto>>> Handle(GetUsersUseCaseQuery request, CancellationToken cancellationToken)
        {
            List<GetUserDto> listOUsers = new List<GetUserDto>();
            var users = await _repository.GetAll();
            var applicationUsers = users as ApplicationUser[] ?? users.ToArray();
            if(!applicationUsers.Any()) return Result.Fail<List<GetUserDto>>("No user found");
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
=======
        public async Task<Result<GetUserDto>> Handle(GetUserByEmailUseCaseQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.Find(predicate: x => x.Email == request.Email);
            if(string.IsNullOrEmpty(user.Email)) return Result.Fail<GetUserDto>("User does not exist");
            return Result.Ok<GetUserDto>(new GetUserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName
            });
>>>>>>> 56eb5a1 (trying)
            
        }
    }   
}