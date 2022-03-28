using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyStore.Data;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyStoreContext _context;

        public HomeController(MyStoreContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _context.Controller = this;
            _logger = logger;
        }

        
        public async Task<IActionResult> Index(string keyWord)
        {

            if (string.IsNullOrWhiteSpace(keyWord))
            {
                ////check user already provide phonenumber
                //var shoppingCart = new ShoppingCart();
                //shoppingCart.OrderHeader = new OrderHeader();
                //shoppingCart.OrderDetails = await _context.Product.Select(i => new OrderDetail()
                //{
                //    ProductId = i.Id,
                //    Price = i.Price,
                //    BonusPercentage = i.BonusPercentage
                //}).ToListAsync();

                var listProduct = new ShoppingCart();
                listProduct.OrderHeader = new OrderHeader();
                listProduct.OrderDetails = new List<OrderDetail>();
                listProduct.Products = await _context.Product.ToListAsync();

                return View(listProduct);
            }
            else
            {
                var listProduct = new ShoppingCart();
                listProduct.OrderHeader = new OrderHeader();
                listProduct.OrderDetails = new List<OrderDetail>();
                listProduct.Products = await _context.Product.Where(m => (m.Name ?? "").Contains(keyWord)).ToListAsync();
                ViewBag.KeyWord = keyWord;

                return View(listProduct);
            }

         }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
