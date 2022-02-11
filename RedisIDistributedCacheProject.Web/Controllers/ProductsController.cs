using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
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

            _distributedCache.SetString("name", "Burak",options);
            await _distributedCache.SetStringAsync("surname", "Hayırlı",options);
            return View();
        }

        public async Task<IActionResult> Show()
        {
            string name = _distributedCache.GetString("name");
            string surname = await _distributedCache.GetStringAsync("surname");
            ViewBag.name = name;
            ViewBag.surname = surname;
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
