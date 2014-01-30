using System;
using System.Threading;
using Core.Data.Repositories.Interfaces;
using Core.Domain;

namespace Core.Services.Concrete
{
    public interface IToDoService
    {
        void Test();
    }
    public class ToDoService :IToDoService
    {
       
        public void Test()
        {
            throw new NotImplementedException();
        }
    }
}
