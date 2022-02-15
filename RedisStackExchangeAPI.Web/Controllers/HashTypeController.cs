using Microsoft.AspNetCore.Mvc;
using RedisStackExchangeAPI.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisStackExchangeAPI.Web.Controllers
{
    public class HashTypeController : BaseController
    {
        //Dictionary
        public string setKey { get; set; } = "hash";
        public HashTypeController(RedisService redisService) : base(redisService)
        {
        }

        public IActionResult Index()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            if (db.KeyExists(setKey))
            {
                db.HashGetAll(setKey).ToList().ForEach(x =>
                {
                    list.Add(x.Name, x.Value);
                });
            }

            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, string value)
        {
            db.HashSet(setKey, name, value);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(string name)
        {
            await db.HashDeleteAsync(setKey, name);
            return RedirectToAction("Index");
        }
    }
}
