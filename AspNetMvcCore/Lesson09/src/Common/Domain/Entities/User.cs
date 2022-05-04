using Domain.Base.Entities;

namespace Domain.Entities;

public class User : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDay { get; set; }
}
