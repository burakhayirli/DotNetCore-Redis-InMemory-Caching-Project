using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisIDistributedCacheProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisIDistributedCacheProject.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache _distributedCache;
        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1)
            };

            //_distributedCache.SetString("name", "Burak",options);
            //await _distributedCache.SetStringAsync("surname", "Hayırlı",options);


            //Complex Types With Json Serialize
            Product product = new Product { Id = 1, Name = "Book", Price = 50 };
            string jsonProduct = JsonConvert.SerializeObject(product);

            Product product2 = new Product { Id = 2, Name = "Book", Price = 50 };
            string jsonProduct2 = JsonConvert.SerializeObject(product2);

            //await _distributedCache.SetStringAsync("product:1", jsonProduct, options);
            //await _distributedCache.SetStringAsync("product:2", jsonProduct2, options);

            //Binary Format
            Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProduct);
            _distributedCache.Set("product:1", byteProduct);

            return View();
        }

        public async Task<IActionResult> Show()
        {
            //string name = _distributedCache.GetString("name");
            //string surname = await _distributedCache.GetStringAsync("surname");
            //ViewBag.name = name;
            //ViewBag.surname = surname;

            //Complex Types Get Json
            //string jsonProduct = await _distributedCache.GetStringAsync("product:1");
            //Product product = JsonConvert.DeserializeObject<Product>(jsonProduct);
            //ViewBag.product = product;

            //Binary Format Deserialize
            Byte[] byteProduct = _distributedCache.Get("product:1");
            string jsonProduct = Encoding.UTF8.GetString(byteProduct);
            Product product = JsonConvert.DeserializeObject<Product>(jsonProduct);
            ViewBag.product = product;

            return View();
        }

        public async Task<IActionResult> Remove()
        {
            _distributedCache.Remove("name");
            await _distributedCache.RemoveAsync("name");
            return View();
        }
    }
}
