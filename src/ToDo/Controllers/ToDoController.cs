using System;
using System.Web.Mvc;
using Core.Domain.Entities;
using Core.Services;
using Core.Services.Concrete;

namespace ToDo.Controllers {
    public class ToDoController : Controller {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService) {
            _toDoService = toDoService;
        }

        public ActionResult Index() {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public JsonResult Add()
        {
            var toDoItem = new ToDoItem()
            {
                Description = "test" + DateTime.Now
            };

            _toDoService.Add(toDoItem);

            return Json("Added", JsonRequestBehavior.AllowGet);
        }
    }
}