namespace Library.Core.Entities.Abstract
{
    public interface ICreatableEntity
    {
        string CreatedBy { get; init; }
        DateTime CreatedDate { get; init; }
    }
}