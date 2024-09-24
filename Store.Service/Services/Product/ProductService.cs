using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.Services.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repoistory<ProductBrand, int>().GetAllAsNoTrackingAsync();
            IReadOnlyList<BrandTypeDetailsDto> mappedBrands = brands.Select(x => new BrandTypeDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
            }).ToList();
            return mappedBrands;
        }

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repoistory<Store.Data.Entities.Product, int>().GetAllAsNoTrackingAsync();
            IReadOnlyList<ProductDetailsDto> mappedProducts = products.Select(x => new ProductDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                PictureUrl = x.PictureUrl,
                Price = x.Price,
                BrandName = x.Brand.Name,
                TypeName = x.Type.Name,
                CreatedAt = x.CreatedAt,
            }).ToList();
            return mappedProducts;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repoistory<Store.Data.Entities.ProductType, int>().GetAllAsNoTrackingAsync();
            IReadOnlyList<BrandTypeDetailsDto> mappedTypes = types.Select(x => new BrandTypeDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
            }).ToList();
            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? productId)
        {
            if (productId is null)
                throw new Exception("Id is Null");
            var product = await _unitOfWork.Repoistory<Store.Data.Entities.Product,int>().GetByIdAsync(productId.Value);
            if(product is null)
                throw new Exception("Product Not Found");
            var mappedProduct = new ProductDetailsDto
            {
                Id= product.Id,
                Name = product.Name,
                Description = product.Description,
                CreatedAt = product.CreatedAt,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                BrandName = product.Brand.Name,
                TypeName = product.Type.Name
            };
            return mappedProduct;
        }
    }
}
