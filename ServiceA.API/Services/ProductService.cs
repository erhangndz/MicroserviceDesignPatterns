using Microsoft.AspNetCore.Mvc;
using ServiceA.API.Models;

namespace ServiceA.API.Services
{
    public class ProductService(HttpClient _client,ILogger<ProductService> _logger)
    {
        
        public async Task<Product> GetProductById(int id)
        {
            var product = await _client.GetFromJsonAsync<Product>($"products/{id}");
            _logger.LogInformation($"Products: {product.Id} - {product.Name}");
            return product;
        }
    }
}
