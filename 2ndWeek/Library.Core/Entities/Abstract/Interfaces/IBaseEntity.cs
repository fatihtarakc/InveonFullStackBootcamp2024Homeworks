namespace Library.Core.Entities.Abstract.Interfaces
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        Status Status { get; set; }
    }
}