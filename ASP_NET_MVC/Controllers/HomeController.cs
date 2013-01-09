﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ASP_NET_MVC.Models;

namespace ASP_NET_MVC.Controllers
{
    public class HomeController : Controller
    {
        ItemRepository _itemRepository;

        public HomeController(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View("Index",_itemRepository.GetAllItems());
        }

        [HttpPost]
        public ActionResult Create(Item item)
        {
            _itemRepository.CreateNewItem(item);
            return RedirectToAction("Index");
        }
    }
}
