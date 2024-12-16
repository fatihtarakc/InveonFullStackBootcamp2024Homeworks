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
                return new ErrorDataResult<AppUserDto>(Message.Account_Email_Has_Already_Exist);

            if (await accountService.AnyAsync(identityUser => identityUser.UserName == appUserAddDto.Username))
                return new ErrorDataResult<AppUserDto>(Message.Account_Username_Has_Already_Exist);

            IdentityUser identityUser = new()
            {
                UserName = appUserAddDto.Username,
                Email = appUserAddDto.Email
            };

            return null;
        }
    }
}