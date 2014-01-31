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

    
        public JsonResult GetItems()
        {
            return Json(_toDoService.GetItems(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveItems(ToDoItem model)
        {
            _toDoService.Delete(model.id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(ToDoItem model)
        {
            _toDoService.Create(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ToDoItem model)
        {
            _toDoService.AddOrUpdate(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}