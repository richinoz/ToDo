using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
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
