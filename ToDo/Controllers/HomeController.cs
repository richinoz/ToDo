using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
              ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
