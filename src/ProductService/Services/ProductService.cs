﻿namespace ProductService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Contracts;
    using Data.Models;
    using Data.ViewModels;

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }

        public async Task<ProductDetailsViewModel> CreateAsync(ProductCreateModel model)
        {
            var product = _mapper.Map<Product>(model);

            await _productRepository.AddAsync(product);

            await _productRepository.SaveChangesAsync();

            return _mapper.Map<ProductDetailsViewModel>(product);
        }

        public async Task<ProductDetailsViewModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDetailsViewModel> UpdateAsync(string id, ProductUpdateModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
