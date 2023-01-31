using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SimpleCardApp.Application.Services;
using SimpleCardApp.Domain.Models;

namespace SimpleCardApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly MongoDbService _mongoDbService;
        public ProductController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet]
        public async Task<ActionResult> SeedFakeProducts()
        {
            if (!(await _mongoDbService.GetCollection<Product>().FindAsync(x => true)).Any())
            {
                _mongoDbService.GetCollection<Product>().InsertOne(new()
                {
                    Id = new Guid(),
                    Name = "mac",
                    Description = "mac",
                    Price = 25000
                });
                _mongoDbService.GetCollection<Product>().InsertOne(new()
                {
                    Id = new Guid(),
                    Name = "iphone",
                    Description = "iphone",
                    Price = 15000
                });
                _mongoDbService.GetCollection<Product>().InsertOne(new()
                {
                    Id = new Guid(),
                    Name = "tablet",
                    Description = "tablet",
                    Price = 20000
                });
                _mongoDbService.GetCollection<Product>().InsertOne(new()
                {
                    Id = new Guid(),
                    Name = "samsung galaxy",
                    Description = "samsung galaxy",
                    Price = 10000
                });
                _mongoDbService.GetCollection<Product>().InsertOne(new()
                {
                    Id = new Guid(),
                    Name = "hp notebook",
                    Description = "hp notebook",
                    Price = 15000
                });
            }
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _mongoDbService.GetCollection<Product>().FindAsync(x => true);
            return Ok(products.ToList());
        }
    }
}
