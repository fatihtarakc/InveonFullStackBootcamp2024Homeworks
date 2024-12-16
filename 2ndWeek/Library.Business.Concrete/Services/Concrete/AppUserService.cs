namespace Library.Business.Concrete.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IAccountService accountService;
        private readonly ICacheService<AppUser> cacheService;
        private readonly IAppUserRepository appUserRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<AppUserService> logger;
        public AppUserService(IAccountService accountService, ICacheService<AppUser> cacheService, IAppUserRepository appUserRepository, IUnitOfWork unitOfWork, ILogger<AppUserService> logger)
        {
            this.accountService = accountService;
            this.cacheService = cacheService;
            this.appUserRepository = appUserRepository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<IDataResult<AppUserDto>> AddAsync(AppUserAddDto appUserAddDto)
        {
            if (await accountService.AnyAsync(identityUser => identityUser.Email == appUserAddDto.Email))
                return new ErrorDataResult<AppUserDto>(Message.Account_Email_Has_Already_Been_Exist);

            if (await accountService.AnyAsync(identityUser => identityUser.UserName == appUserAddDto.Username))
                return new ErrorDataResult<AppUserDto>(Message.Account_Username_Has_Already_Been_Exist);

            IdentityUser identityUser = new()
            {
                UserName = appUserAddDto.Username,
                Email = appUserAddDto.Email
            };

            return null;
        }
    }
}