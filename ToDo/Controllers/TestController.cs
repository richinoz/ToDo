﻿using System.Web.Mvc;
using Core.Services;

namespace ToDo.Controllers
{
    public class TestController : Controller
    {
        private readonly IToDoService _toDoService;

        public TestController(IToDoService toDoService)
        {
            _toDoService = toDoService;
            
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View("");
        }
    }
}