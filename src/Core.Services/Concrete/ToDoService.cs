using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Data.Repositories.Interfaces;
using Core.Domain;
using Core.Domain.Entities;

namespace Core.Services.Concrete
{
    public interface IToDoService
    {
        void AddOrUpdate(ToDoItem item);
        void Delete(int itemId);
        IList<ToDoItem> GetItems();
        ToDoItem Create(ToDoItem model);
    }
    public class ToDoService :IToDoService
    {
        private readonly IRepository<ToDoItem> _todoRepository;

        public ToDoService(IRepository<ToDoItem> todoRepository) {
            _todoRepository = todoRepository;
        }

     
        public void AddOrUpdate(ToDoItem item)
        {
            _todoRepository.Save(item);
        }

        public void Delete(int itemId)
        {
            _todoRepository.DeleteBy(x=>x.id==itemId);
        }

        public IList<ToDoItem> GetItems()
        {
            return _todoRepository.All().ToList();
        }

        public ToDoItem Create(ToDoItem model) {
            return _todoRepository.Add(model);
        }
       
    }
}
