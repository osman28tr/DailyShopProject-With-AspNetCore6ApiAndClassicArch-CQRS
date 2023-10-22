namespace Core.Persistence.Repositories;

public class Entity
{
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public Entity()
    {
    }

    public Entity(int id) : this()
    {
        Id = id;
    }
}