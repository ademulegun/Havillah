using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.Product.UseCases.AddProduct.Dto;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;

namespace Havillah.ApplicationServices.Product.AddProduct.Handlers;

public class AddProductCommand : IRequest<Result>
{
    public Havillah.Shared.Product.AddProductDto AddProductDto { get; set; }
    
    public class AddProductCommandHandler: IRequestHandler<AddProductCommand, Result>
    {
        private readonly IRepository<Core.Domain.Product> _repository;
        private readonly IRepository<Core.Domain.ProductCategory> _categoryRepository;
        private readonly IUploadImageToStorage _uploadImage;
        public AddProductCommandHandler(IRepository<Core.Domain.Product> repository, IUploadImageToStorage uploadImage, IRepository<ProductCategory> categoryRepository)
        {
            _repository = repository;
            _uploadImage = uploadImage;
            _categoryRepository = categoryRepository;
        }
        public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productId = Guid.NewGuid();
                //Call cloud storage here, then return url to product image.
                //var result = await _uploadImage.Upload(request.AddProductDto.ProductImage);
                var category = await _categoryRepository.Find(predicate: x => x.Id == request.AddProductDto.CategoryId);
                var productCode = _repository.GetAll().Result.Last();
                var product = Core.Domain.Product.ProductFactory.Create(productId, request.AddProductDto.ProductName, productCode.ProductCode,
                        request.AddProductDto.Description, "", request.AddProductDto.UnitOfMeasureId, request.AddProductDto.DefaultBuyingPrice,
                        request.AddProductDto.DefaultSellingPrice, request.AddProductDto.ProductImage, request.AddProductDto.ProductImageLength, 
                        request.AddProductDto.ProductImageExtension, request.AddProductDto.Colours, request.AddProductDto.Sizes, 
                        request.AddProductDto.BrandName, request.AddProductDto.Quantity, category).SetBranchId(request.AddProductDto.BranchId)
                    .SetCurrencyId(request.AddProductDto.CurrencyId)
                    .SetUnitOfMeasureId(request.AddProductDto.UnitOfMeasureId);
                await _repository.Add(model: product);
                var productSaved = await _repository.Save();
                return productSaved > 0 ? Result.Ok("Product added successfully", "00") : Result.Fail("Unable to add product", "01");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result.Fail("Something went wrong", "01");
            }
        }
    }   
}