using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.Product.UseCases.GetProduct.Dto;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.Product.UseCases.GetProduct.Handlers;

public class GetProductQuery: IRequest<Result<GetProductDto>>
{
    public Guid Id { get; set; }
    
    public class GetProductQueryHandle: IRequestHandler<GetProductQuery, Result<GetProductDto>>
    {
        private readonly IRepository<Core.Domain.Product> _repository;
        public GetProductQueryHandle(IRepository<Core.Domain.Product> repository)
        {
            _repository = repository;
        }
        
        public async Task<Result<GetProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var productFromDb = await _repository.Find(x=>x.Id == request.Id);
            if(productFromDb.Id == default) return Result.Fail<GetProductDto>("Not Found");
            var product = new GetProductDto()
            {
                Id = productFromDb.Id.ToString(), ProductName = productFromDb.ProductName, DefaultBuyingPrice = productFromDb.BuyingPrice, 
                DefaultSellingPrice = productFromDb.SellingPrice, Barcode = productFromDb.Barcode, Description = productFromDb.Description, 
                BranchId = productFromDb.BranchId, CurrencyId = productFromDb.CurrencyId, ProductCode = productFromDb.ProductCode, 
                ProductImage = productFromDb.ProductImage, ProductImageExtension = productFromDb.ProductImageExtension,
                Colours = productFromDb.Colours, Sizes = productFromDb.Sizes, BrandName = productFromDb.BrandName
            };
            return Result.Ok(product);
        }
    }   
}