using Havillah.ApplicationServices.Interfaces;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.Category.UseCases.AddCategory.Handler;

public class AddCategoryCommand: IRequest<Result>
{
    public string Name { get; set; }
    
    public class AddCategoryCommandHandler: IRequestHandler<AddCategoryCommand, Result>
    {
        private readonly IRepository<Core.Domain.ProductCategory> _repository;
        public AddCategoryCommandHandler(IRepository<ProductCategory> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Add(new ProductCategory(request.Name));
                var result = await _repository.Save();
                return result>0 ? Result.Ok("Category created successfully", "00") : Result.Fail("Unable to save product category", "01");
            }
            catch (Exception e)
            {
                return Result.Fail("Unable to save product category, something went wrong", "01");
            }
        }
    } 
}