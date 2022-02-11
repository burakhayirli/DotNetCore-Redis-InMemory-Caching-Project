using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisIDistributedCacheProject.Web.Controllers
{
    public class ProductsController1 : Controller
    {
        private IDistributedCache _distributedCache;
        public ProductsController1(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
