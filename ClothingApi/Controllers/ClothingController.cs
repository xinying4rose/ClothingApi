using ClothingApi.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingApi.Models
{
    [Route("api/[controller]")]
    public class ClothingController : Controller
    {
        private readonly ClothingShopDbContext clothingShopDbContext;
        public ClothingController(ClothingShopDbContext clothingShopDbContext)
        {
            this.clothingShopDbContext = clothingShopDbContext;
        }


        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ClothingProduct>> GetAll([FromQuery] string? filter, [FromQuery] SortingOption? sortingOption)
        {
            IEnumerable<ClothingProduct> products;

            if (filter != null)
            {
                filter = filter.ToLower();

                products = await clothingShopDbContext.Product
                    .Where(prod => prod.Name.ToLower().IndexOf(filter) >= 0).ToListAsync();
            }
            else
            {
                products = await clothingShopDbContext.Product.ToListAsync();
            }

            // Sort
            switch (sortingOption)
            {
                case SortingOption.PriceAscending:
                    return products.OrderBy(prod => prod.Price);

                case SortingOption.PriceDescending:
                    return products.OrderByDescending(prod => prod.Price);

                default:
                    return products;
            }

            
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateAsync(ClothingProduct newProduct)
        {
            // Add the new product to DbContext
            await clothingShopDbContext.Product.AddAsync(newProduct);

            // Save DbContext
            await clothingShopDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
