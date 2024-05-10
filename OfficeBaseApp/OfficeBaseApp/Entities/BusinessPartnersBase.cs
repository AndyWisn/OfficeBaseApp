

namespace OfficeBaseApp.Entities
{
    public class BusinessPartnersBase : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? RepresentativeFirstName { get; set; }
        public string? RepresentativeLastName { get; set; }
        public string? Contact {  get; set; }
        public string? Address { get; set; }
        public override string ToString() => $"Id: {Id} | Company: {Name} | {RepresentativeFirstName} {RepresentativeFirstName} Contact: {Contact}";
    }
}
