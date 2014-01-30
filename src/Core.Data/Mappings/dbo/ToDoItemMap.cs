using System;
using Core.Domain.Entities;
using NHibernate.Mapping;

namespace Core.Data.Mappings.dbo {
    public class ToDoItemMap :SchemaClassMap<ToDoItem>
    {
        public ToDoItemMap()
        {            			
			LazyLoad();
			Id(x => x.Id).GeneratedBy.Assigned();
			Map(x => x.Description);
        }
    }
}