namespace Havillah.Core.Domain;

public class Customer: BaseEntity<Guid>
{
    public Customer() { }
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public bool IsWhatsAppEnabled { get; private set; }
}