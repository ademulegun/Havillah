using Havillah.ApplicationServices.Product.AddProduct.Dto;
using MediatR;

namespace Havillah.ApplicationServices.Product.AddProduct.Handlers;

public class AddProduct : IRequest<string>
{
    public AddProductDto AddProductDto { get; set; }
    public class AddProductHandler: IRequestHandler<AddProduct, string>
    {
        public Task<string> Handle(AddProduct request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }   
}