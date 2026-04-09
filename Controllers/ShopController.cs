using Microsoft.AspNetCore.Mvc;

namespace fourth_term_software_labs.Controllers
{
    [Route("store")]
    [Route("shop")]
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.StoreName = "Магазин 'У Михаилыча'";
            ViewData["ProductsCount"] = 15;

            return View();
        }

        [Route("category/{categoryName}")]
        public IActionResult Category(string categoryName)
        {
            ViewBag.Category = categoryName;
            ViewBag.Products = new[] { "Ноутбук", "Смартфон", "Планшет" };

            return View();
        }
        [Route("product/{id}/details")]
        public IActionResult ProductDetails(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.ProductName = $"Товар #{id}";

            return View();
        }
    }
}
