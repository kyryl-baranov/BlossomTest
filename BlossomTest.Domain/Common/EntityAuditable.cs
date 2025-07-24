namespace BlossomTest.Domain.Common;

public class EntityAuditable : Entity
{
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}
