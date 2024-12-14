namespace Library.Core.Entities.Abstract.Interfaces
{
    public interface ICreatableEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
    }
}