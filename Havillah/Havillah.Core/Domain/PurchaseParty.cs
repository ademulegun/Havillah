namespace Havillah.Core.Domain;

public class PurchaseParty: BaseEntity<Guid>
{
    public PurchaseParty() { }
    public string PartyName { get; private set; }
    public string Email { get; private set; }
    public string Website { get; private set; }
    public string PhoneNumber { get; private set; }
}