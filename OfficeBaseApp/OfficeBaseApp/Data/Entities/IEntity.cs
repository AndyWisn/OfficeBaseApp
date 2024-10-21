namespace OfficeBaseApp.Data.Entities;

public interface IEntity
{
    int Id { get; set; }
    string? Name { get; set; }
    string? Description { get; set; }
    void EnterPropertiesFromConsole();
}