using Microsoft.AspNetCore.Mvc;
using RedisStackExchangeAPI.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisStackExchangeAPI.Web.Controllers
{
    public class SortedSetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string setKey = "sortedset";
        public SortedSetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(3);
        }
        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();
            if (db.KeyExists(setKey))
            {
                //Default ASC
                db.SortedSetScan(setKey).ToList().ForEach(x =>
                {
                    //x.Element
                    //x.Score
                    list.Add(x.Element.ToString());
                });

                //DESC
                db.SortedSetRangeByRank(setKey, 0, 5, order: Order.Descending).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });
            }

            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, int score)
        {
            db.SortedSetAdd(setKey, name, score);
            db.KeyExpire(setKey, DateTime.Now.AddMinutes(2));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(string name)
        {
            await db.SortedSetRemoveAsync(setKey, name);
            return RedirectToAction("Index");
        }
    }
}
