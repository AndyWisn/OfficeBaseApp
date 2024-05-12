

namespace OfficeBaseApp.Entities
{
    public abstract class BusinessPartnersBase : IEntity
    {
        public BusinessPartnersBase()
        {
        }
        public BusinessPartnersBase(string name)
        {
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? RepresentativeFirstName { get; set; }
        public string? RepresentativeLastName { get; set; }
        public string? Contact { get; set; }
        //public string? Address { get; set; }

        public override string ToString() => string.Format("{0,-8} {1,-30} {2,-30} {3,-10}",$"|Id:{Id}",$"|Name: {Name}",$"|Rep: {RepresentativeFirstName} {RepresentativeLastName}", $"|{Contact}");
    }
}
