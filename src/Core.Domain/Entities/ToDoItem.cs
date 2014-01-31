namespace Core.Domain.Entities
{
    public class ToDoItem
    {
        public virtual int id { get; set; }
        //public virtual int Status { get; set; }
        public virtual bool completed { get; set; }
        public virtual bool edit { get; set; }
        public virtual string title { get; set; }
        //public virtual string Description { get; set; }
      
    }
}
