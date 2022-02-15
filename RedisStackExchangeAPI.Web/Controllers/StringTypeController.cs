using Microsoft.AspNetCore.Mvc;
using RedisStackExchangeAPI.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisStackExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {                   
            db.StringSet("name", "Burak HAYIRLI");
            db.StringSet("vizitor", 100);

            return View();
        }

        public IActionResult Show()
        {
            //var value = db.StringGet("name");
            //db.StringIncrement("vizitor",10);
            //var vizitorCount=db.StringDecrementAsync("vizitor", 1).Result;
            //db.StringDecrementAsync("vizitor", 5).Wait();

            //var value = db.StringGetRange("name",0,3);

            //if (value.HasValue)
            //{
            //    ViewBag.value = value.ToString();
            //}

            var value = db.StringLength("name");
            ViewBag.value = value.ToString();

            Byte[] photoByte = default(byte[]);
            db.StringSet("photo", photoByte);

            return View();
        }

        
    }
}
