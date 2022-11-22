namespace Havillah.Core.Domain;

public abstract class BaseEntity<T>
{
    protected BaseEntity(){}
    private BaseEntity(T id)
    {
        if (default(T).Equals(id))
        {
            throw new Exception("Id is invalid");
        }
    }
    protected T Id { get; set; }
    public DateTime DateAdded { get; set; }
}