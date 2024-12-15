using Library.Business.Constants;
using Library.Core.Utilities.Results.Concrete;

namespace Library.Business.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IAccountService accountService;
        private readonly IAppUserRepository appUserRepository;
        private readonly ILogger<AppUserService> logger;
        private readonly IUnitOfWork unitOfWork;
        public AppUserService(IAccountService accountService, IAppUserRepository appUserRepository, ILogger<AppUserService> logger, IUnitOfWork unitOfWork)
        {
            this.accountService = accountService;
            this.appUserRepository = appUserRepository;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<AppUserDto>> AddAsync(AppUserAddDto appUserAddDto)
        {
            if (await accountService.AnyAsync(identityUser => identityUser.Email == appUserAddDto.Email))
                return new ErrorDataResult<AppUserDto>(Message.ACCOUNT_NOT_FOUND);

            return null;
        }
    }
}