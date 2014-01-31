using System;
using Core.Domain.Entities;
using NHibernate.Mapping;

namespace Core.Data.Mappings.dbo {
    public class ToDoItemMap :SchemaClassMap<ToDoItem>
    {
        public ToDoItemMap()
        {            			
			LazyLoad();
			Id(x => x.id).GeneratedBy.Assigned();
			Map(x => x.completed);
			Map(x => x.title);
			Map(x => x.edit);
        }
    }
}