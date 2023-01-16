using Havillah.ApplicationServices.Interfaces;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.Product.UseCases.UpdateProduct.Handlers;

public class UpdateProductProductCommand : IRequest<Result>
{
    public Havillah.Shared.Product.UpdateProductDto ProductDto { get; set; }
    
    public class UpdateProductProductCommandHandler: IRequestHandler<UpdateProductProductCommand, Result>
    {
        private readonly IRepository<Core.Domain.Product> _repository;
        private readonly IUploadImageToStorage _uploadImage;
        public UpdateProductProductCommandHandler(IRepository<Core.Domain.Product> repository, IUploadImageToStorage uploadImage)
        {
            _repository = repository;
            _uploadImage = uploadImage;
        }
        public async Task<Result> Handle(UpdateProductProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productFromDb = await _repository.Find(x=>x.Id == Guid.Parse(request.ProductDto.Id));
                if(productFromDb.Id == default) return Result.Fail("Not Found", "404");
                //Call cloud storage here, then return url to product image.
                //var result = await _uploadImage.Upload(request.AddProductDto.ProductImage);
                productFromDb.SetBrandName(request.ProductDto.BrandName).SetProductName(request.ProductDto.ProductName).SetColour(request.ProductDto.Colours)
                    .SetDescription(request.ProductDto.Description).SetSize(request.ProductDto.Sizes).SetBuyingPrice(request.ProductDto.DefaultBuyingPrice)
                    .SetSellingPrice(request.ProductDto.DefaultSellingPrice).SetImageByte(request.ProductDto.ProductImage).SetImageExtension(request.ProductDto.ProductImageExtension)
                    .SetImageLength(request.ProductDto.ProductImageLength);
                _repository.Update(model: productFromDb);
                var productSaved = await _repository.Save();
                return productSaved > 0 ? Result.Ok("Product updated successfully", "00") : Result.Fail("Unable to update product", "01");
            }
            catch (Exception e)
            {
                return Result.Fail("Something went wrong", "01");
            }
        }
    }   
}