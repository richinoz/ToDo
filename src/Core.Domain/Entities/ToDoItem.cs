namespace Core.Domain.Entities
{
    public class ToDoItem
    {
        public virtual int Id { get; set; }
        public virtual int Status { get; set; }
        public virtual string Description { get; set; }
    }
}
