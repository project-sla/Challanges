using System.ComponentModel.DataAnnotations;

namespace Challenges.Domain.Entities.Common;

public class Base
{
    private Base()
    {
    }

    public Base(Guid? id = null, Guid createdBy = default)
    {
        Id = id ?? Guid.NewGuid();
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    public Guid Id { get; private init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    
    public void Delete()
    {
        IsDeleted = true;
    }
    
    public void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}