using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.Product.UseCases.AddProduct.Dto;
using MediatR;

namespace Havillah.ApplicationServices.Product.AddProduct.Handlers;

public class AddProductCommand : IRequest<string>
{
    public AddProductDto AddProductDto { get; set; }
    
    public class AddProductCommandHandler: IRequestHandler<AddProductCommand, string>
    {
        private readonly IRepository<Core.Domain.Product> _repository;
        public AddProductCommandHandler(IRepository<Core.Domain.Product> repository)
        {
            _repository = repository;
        }
        public async Task<string> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productId = Guid.NewGuid();
            var product = Core.Domain.Product.ProductFactory.Create(productId, request.AddProductDto.ProductName, request.AddProductDto.ProductCode,
                request.AddProductDto.Description, request.AddProductDto.ProductImageUrl, request.AddProductDto.UnitOfMeasureId, request.AddProductDto.DefaultBuyingPrice,
                request.AddProductDto.DefaultSellingPrice).SetBranchId(request.AddProductDto.BranchId)
                .SetCurrencyId(request.AddProductDto.CurrencyId)
                .SetUnitOfMeasureId(request.AddProductDto.UnitOfMeasureId);
            await _repository.Add(model: product);
            return await _repository.Save() > 1 ? "Product added successfully" : "Unable to add product";
        }
    }   
}