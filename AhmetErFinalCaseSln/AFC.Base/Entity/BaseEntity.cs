namespace AFC.Base.Entity;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public int CreateBy { get; set; }
    public DateTime CreateAt { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsActive { get; set; }
}
