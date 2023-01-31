using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SimpleCardApp.Application.Services;
using SimpleCardApp.Domain.Models;

namespace SimpleCardApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        readonly RedisService _redisService;
        readonly MongoDbService _mongoDbService;
        public CardController(RedisService redisService, MongoDbService mongoDbService)
        {
            _redisService = redisService;
            _mongoDbService = mongoDbService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">cannot be 0</param>
        /// <param name="cardProducts">must be the product ids from return /product/getproducts, count cannnot be 0</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCard(int userId, [FromBody] List<CardProduct> cardProducts)
        {
            try
            {
                foreach (var item in cardProducts)
                {
                    if(item.Count == 0)
                        return BadRequest("Count cannot be '0' : " + item.ProductId);
                    if (!_mongoDbService.GetCollection<Product>().Find(_ => _.Id == item.ProductId).Any())
                        return BadRequest("ProductNotFound : "+ item.ProductId);
                }

                var userCard = _redisService.GetData<Card>("userId=" + userId);
                if (userCard == null)
                    _redisService.SetData<Card>("userId=" + userId, new Card { CardProducts = cardProducts, UserId = userId }, DateTimeOffset.Now.AddDays(1));
                else
                {
                    foreach (var cardProduct in cardProducts)
                    {
                        if (userCard.CardProducts.Any(a => a.ProductId == cardProduct.ProductId))
                        {
                            userCard.CardProducts.FirstOrDefault(a => a.ProductId == cardProduct.ProductId).Count += cardProduct.Count;
                        }
                        else
                        {
                            userCard.CardProducts.Add(cardProduct);
                        }
                    }

                    _redisService.RemoveData("userId=" + userId);
                    _redisService.SetData<Card>("userId=" + userId, userCard, DateTimeOffset.Now.AddDays(1));
                }
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpGet]
        public ActionResult GetCard(int userId)
        {
            if (userId == 0) return BadRequest("no zero");
            return Ok(_redisService.GetData<Card>("userId=" + userId));
        }
    }
}
