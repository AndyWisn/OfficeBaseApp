namespace OfficeBaseApp.Entities;
public abstract class BusinessPartnersBase : IEntity
{
    public int Id { get; set; } 
    public string? Name { get; set; } 
    public string? RepresentativeFirstName { get; set; }
    public string? RepresentativeLastName { get; set; }
    public string? Contact { get; set; }
    public override string ToString() => string.Format("{0,-10} {1,-30} {2,-30} {3,-10}", $"|Id:{Id}", $"|Name: {Name}", $"|Rep: {RepresentativeFirstName} {RepresentativeLastName}", $"|{Contact}");
    public abstract void EnterPropertiesFromConsole();
    
}