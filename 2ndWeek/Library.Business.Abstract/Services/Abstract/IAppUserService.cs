namespace Library.Business.Abstract.Services.Abstract
{
    public interface IAppUserService
    {
        Task<IDataResult<AppUserDto>> AddAsync(AppUserAddDto appUserAddDto);
    }
}