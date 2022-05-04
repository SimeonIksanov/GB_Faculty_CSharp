using Interfaces.Base.Entities;

namespace Domain.Base.Entities;

public abstract class Entity : IEntity
{
    public int Id { get ; set ; }
}
