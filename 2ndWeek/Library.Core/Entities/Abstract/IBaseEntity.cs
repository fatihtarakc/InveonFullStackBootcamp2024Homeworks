namespace Library.Core.Entities.Abstract
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        Status Status { get; set; }
    }
}