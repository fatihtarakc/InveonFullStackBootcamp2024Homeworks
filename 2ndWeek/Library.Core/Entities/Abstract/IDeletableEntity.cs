namespace Library.Core.Entities.Abstract
{
    public interface IDeletableEntity
    {
        string? DeletedBy { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}