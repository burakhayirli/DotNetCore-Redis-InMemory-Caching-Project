using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryProject.Web.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            //if (String.IsNullOrEmpty(_memoryCache.Get<string>("time")))
            //{
            //    _memoryCache.Set<string>("time", DateTime.Now.ToString());
            //}

            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1),
                SlidingExpiration = TimeSpan.FromSeconds(10),
                Priority = CacheItemPriority.High
            };

            _memoryCache.Set<string>("time", DateTime.Now.ToString(), options);

            return View();
        }

        public IActionResult Show()
        {
            //_memoryCache.GetOrCreate<string>("time", entry =>
            //{
            //    return DateTime.Now.ToString();
            //});

            _memoryCache.TryGetValue("time", out string timeCache);
            ViewBag.time = timeCache;
            //ViewBag.time = _memoryCache.Get<string>("time");

            return View();
        }
    }
}
