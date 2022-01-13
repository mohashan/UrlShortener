public class BaseEntity
{
    public int Id { get; set; }
    public DateTimeOffset InsertDateTime { get; set; } = DateTimeOffset.Now;
}
