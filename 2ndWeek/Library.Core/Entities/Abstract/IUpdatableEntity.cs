namespace Library.Core.Entities.Abstract
{
    public interface IUpdatableEntity
    {
        string? ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}