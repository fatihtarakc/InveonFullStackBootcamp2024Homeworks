namespace Library.Business.Services.Abstract
{
    public interface IAppUserService
    {
        Task<IDataResult<AppUserDto>> AddAsync(AppUserAddDto appUserAddDto);
    }
}