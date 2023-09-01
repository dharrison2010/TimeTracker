namespace TimeTracker.Shared.Entities;
public class SoftDeletableEntity : BaseEntity
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DateDeleted { get; set; }
}
