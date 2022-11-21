using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.Product.AddProduct.Dto;
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
            var product = Core.Domain.Product.ProductFactory.Create(productId, "", "", "", "", 1, 0.0, 0.0);
            await _repository.Add(model: product);
            var result = await _repository.Save();
            return "";
        }
    }   
}