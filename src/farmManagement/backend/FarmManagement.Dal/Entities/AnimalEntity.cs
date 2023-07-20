namespace FarmManagement.Dal.Entities;

public sealed class AnimalEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;
}