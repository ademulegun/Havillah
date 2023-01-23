using Havillah.ApplicationServices.Category.UseCases.Dto;
using Havillah.ApplicationServices.Interfaces;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.Category.UseCases.GetCategories.Handler;

public class GetCategoryQuery: IRequest<Result<List<GetCategoryDto>>>
{
    public class GetCategoryQueryHandler: IRequestHandler<GetCategoryQuery, Result<List<GetCategoryDto>>>
    {
        private readonly IRepository<ProductCategory> _repository;
        public GetCategoryQueryHandler(IRepository<ProductCategory> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetCategoryDto>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categoriesList = await _repository.GetAll();
                var productCategories = categoriesList as ProductCategory[] ?? categoriesList.ToArray();
                if (productCategories.Any())
                {
                    return Result.Ok(productCategories.Select(x=> new GetCategoryDto()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList());
                }
                return Result.Ok<List<GetCategoryDto>>(new List<GetCategoryDto>(), "00");
            }
            catch (Exception e)
            {
                return Result.Fail<List<GetCategoryDto>>("An error occured while getting all categories", "01");
            }
        }
    } 
}