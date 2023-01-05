<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> e34493e (modified espense with constructor)
using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.User.Dto;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;

<<<<<<< HEAD
namespace Havillah.ApplicationServices.User.UseCases.Queries;

public class GetUserByEmailUseCaseQuery: IRequest<Result<GetUserDto>>
{
    public string Email { get; set; } = null!;
    
    public class GetUserByEmailUseCaseQueryHandler: IRequestHandler<GetUserByEmailUseCaseQuery, Result<GetUserDto>>
    {
        private readonly IRepository<ApplicationUser> _repository;
        public GetUserByEmailUseCaseQueryHandler(IRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }
        
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
            
        }
    }   
=======
=======
>>>>>>> e34493e (modified espense with constructor)
namespace Havillah.ApplicationServices.User.UseCases.Queries;

public class GetUserByEmailUseCaseQuery: IRequest<Result<GetUserDto>>
{
    public string Email { get; set; } = null!;
    
<<<<<<< HEAD
>>>>>>> 56eb5a1 (trying)
=======
    public class GetUserByEmailUseCaseQueryHandler: IRequestHandler<GetUserByEmailUseCaseQuery, Result<GetUserDto>>
    {
        private readonly IRepository<ApplicationUser> _repository;
        public GetUserByEmailUseCaseQueryHandler(IRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }
        
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
            
        }
    }
>>>>>>> e34493e (modified espense with constructor)
}