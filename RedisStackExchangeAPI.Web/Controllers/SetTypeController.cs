using Microsoft.AspNetCore.Mvc;
using RedisStackExchangeAPI.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisStackExchangeAPI.Web.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string setKey = "setnames";
        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(2);
        }
        public IActionResult Index()
        {
            //HashSet contains unique values instead of List
            HashSet<string> namesList = new HashSet<string>();
            if (db.KeyExists(setKey))
            {
                db.SetMembers(setKey).ToList().ForEach(x=> {
                    namesList.Add(x.ToString());
                });
            }
            return View(namesList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            //AbsoluteExpiration Enabled
            //SlidingExpiration Disabled
            //if (!db.KeyExists(setKey))
            //{
            //    db.KeyExpire(setKey, DateTime.Now.AddMinutes(5));
            //}
            
            //SlidingExpiration Enabled
            db.KeyExpire(setKey, DateTime.Now.AddMinutes(5));

            //Adds randomly index
            db.SetAdd(setKey, name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(string name)
        {
            await db.SetRemoveAsync(setKey,name);
            return RedirectToAction("Index");
        }
    }
}
