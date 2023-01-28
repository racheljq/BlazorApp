using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        [Route("getProducts")]
        public IActionResult getProducts()
        {
            List<Product> _productList = new List<Product>();

            Product p = null;
            p = new Product { Id = 1, Name = "Product 1", Brand = "Brand 1", Price = 1, Quantity = 1 };
            _productList.Add(p);

            p = new Product { Id = 2, Name = "Product 2", Brand = "Brand 2", Price = 1, Quantity = 2 };
            _productList.Add(p);

            p = new Product { Id = 3, Name = "Product 3", Brand = "Brand 3", Price = 1, Quantity = 3 };
            _productList.Add(p);

            p = new Product { Id = 1, Name = "Product 4", Brand = "Brand 4", Price = 1, Quantity = 4 };
            _productList.Add(p);

            return Ok(_productList);

        }


    }
}
