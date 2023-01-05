using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.User.Dto;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.User.UseCases.Queries;

<<<<<<< HEAD
public class GetUserByIdUseCaseQuery: IRequest<Result<GetUserDto>>
{
    public Guid Id { get; set; }
    
    public class GetUserByIdUseCaseQueryHandler: IRequestHandler<GetUserByIdUseCaseQuery, Result<GetUserDto>>
    {
        private readonly IRepository<ApplicationUser> _repository;
        public GetUserByIdUseCaseQueryHandler(IRepository<ApplicationUser> repository)
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
        public async Task<Result<GetUserDto>> Handle(GetUserByIdUseCaseQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.Find(predicate: x => x.Id == request.Id);
=======
        public async Task<Result<GetUserDto>> Handle(GetUserByEmailUseCaseQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.Find(predicate: x => x.Email == request.Email);
>>>>>>> 56eb5a1 (trying)
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
}