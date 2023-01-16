using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.User.Dto;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.User.UseCases.Queries;

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 76516a4 (few changes done here for dev)
public class GetUsersUseCaseQuery: IRequest<Result<List<GetUserDto>>>
{

    public class GetUsersUseCaseQueryHandler: IRequestHandler<GetUsersUseCaseQuery, Result<List<GetUserDto>>>
    {
        private readonly IRepository<ApplicationUser> _repository;
        public GetUsersUseCaseQueryHandler(IRepository<ApplicationUser> repository)
=======
public class GetUserByEmailUseCaseQuery: IRequest<Result<GetUserDto>>
=======
public class GetUsersUseCaseQuery : IRequest<Result<List<GetUserDto>>>
>>>>>>> e34493e (modified espense with constructor)
{

    public class GetUsersUseCaseQueryHandler : IRequestHandler<GetUsersUseCaseQuery, Result<List<GetUserDto>>>
    {
        private readonly IRepository<ApplicationUser> _repository;
<<<<<<< HEAD
        public GetUserByEmailUseCaseQueryHandler(IRepository<ApplicationUser> repository)
>>>>>>> 56eb5a1 (trying)
        {
            _repository = repository;
        }
        
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 76516a4 (few changes done here for dev)
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
<<<<<<< HEAD
=======
        public async Task<Result<GetUserDto>> Handle(GetUserByEmailUseCaseQuery request, CancellationToken cancellationToken)
=======
        public GetUsersUseCaseQueryHandler(IRepository<ApplicationUser> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetUserDto>>> Handle(GetUsersUseCaseQuery request, CancellationToken cancellationToken)
>>>>>>> e34493e (modified espense with constructor)
        {
            List<GetUserDto> listOUsers = new List<GetUserDto>();
            var users = await _repository.GetAll();
            var applicationUsers = users as ApplicationUser[] ?? users.ToArray();
            if (!applicationUsers.Any()) return Result.Fail<List<GetUserDto>>("No user found");
            foreach (var user in applicationUsers)
            {
<<<<<<< HEAD
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName
            });
>>>>>>> 56eb5a1 (trying)
            
=======
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

>>>>>>> e34493e (modified espense with constructor)
        }
    }
=======
            
        }
    }   
>>>>>>> 76516a4 (few changes done here for dev)
}