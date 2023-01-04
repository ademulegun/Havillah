using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.Product.UseCases.GetProduct.Dto;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.Product.UseCases.GetProduct.Handlers;

public class GetProductsQuery: IRequest<Result<List<GetProductDto>>>
{
    public class GetProductsQueryHandle: IRequestHandler<GetProductsQuery, Result<List<GetProductDto>>>
    {
        private readonly IRepository<Core.Domain.Product> _repository;
        public GetProductsQueryHandle(IRepository<Core.Domain.Product> repository)
        {
            _repository = repository;
        }
        
        public async Task<Result<List<GetProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _repository.GetAll();
                if(!products.Any()) return Result.Ok<List<GetProductDto>>(new List<GetProductDto>());
                List<GetProductDto> productsList = products.Select(x => new GetProductDto()
                {
                    Id = x.Id.ToString(), ProductName = x.ProductName, DefaultBuyingPrice = x.BuyingPrice, DefaultSellingPrice = x.SellingPrice,
                    Barcode = x.Barcode, Description = x.Description, BranchId = x.BranchId, CurrencyId = x.CurrencyId, 
                    ProductCode = x.ProductCode, ProductImage = x.ProductImage, ProductImageExtension = x.ProductImageExtension,
                    Colours = x.Colours, Sizes = x.Sizes, BrandName = x.BrandName
                }).ToList();
                return Result.Ok(productsList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result.Fail<List<GetProductDto>>("An error occured");
            }
        }
    }   
}