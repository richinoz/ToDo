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
        void Add(ToDoItem item);
        IList<ToDoItem> GetItems();
    }
    public class ToDoService :IToDoService
    {
        private readonly IRepository<ToDoItem> _todoRepository;

        public ToDoService(IRepository<ToDoItem> todoRepository) {
            _todoRepository = todoRepository;
        }

     
        public void Add(ToDoItem item)
        {
            _todoRepository.Save(item);
        }

        public IList<ToDoItem> GetItems()
        {
            return _todoRepository.All().ToList();
        }
    }
}
