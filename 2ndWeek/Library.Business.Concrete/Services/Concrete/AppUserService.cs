namespace Library.Business.Concrete.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IAccountService accountService;
        private readonly ICacheService<AppUser> cacheService;
        private readonly IAppUserRepository appUserRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IStringLocalizer<MessageResources> stringLocalizer;
        private readonly ILogger<AppUserService> logger;
        public AppUserService(IAccountService accountService, ICacheService<AppUser> cacheService, IAppUserRepository appUserRepository, IUnitOfWork unitOfWork, IStringLocalizer<MessageResources> stringLocalizer, ILogger<AppUserService> logger)
        {
            this.accountService = accountService;
            this.cacheService = cacheService;
            this.appUserRepository = appUserRepository;
            this.unitOfWork = unitOfWork;
            this.stringLocalizer = stringLocalizer;
            this.logger = logger;
        }

        public async Task<IDataResult<AppUserDto>> AddAsync(AppUserAddDto appUserAddDto)
        {
            if (await accountService.AnyAsync(identityUser => identityUser.Email == appUserAddDto.Email))
                return new ErrorDataResult<AppUserDto>(stringLocalizer[Messages.Account_Email_Has_Already_Been_Exist]);

            if (await accountService.AnyAsync(identityUser => identityUser.UserName == appUserAddDto.Username))
                return new ErrorDataResult<AppUserDto>(stringLocalizer[Messages.Account_Username_Has_Already_Been_Exist]);

            IdentityUser identityUser = new()
            {
                UserName = appUserAddDto.Username,
                Email = appUserAddDto.Email,
                EmailConfirmed = false
            };

            IDataResult<AppUserDto> result = new ErrorDataResult<AppUserDto>();
            var strategy = await unitOfWork.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using var transactionScope = await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                try
                {
                    var identityResult = await accountService.AddAsync(identityUser, Roles.AppUser);
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorDataResult<AppUserDto>(identityResult.ToString());
                        await transactionScope.RollbackAsync();
                        return;
                    }

                    var appUser = new AppUser { IdentityId = identityUser.Id };
                    appUserAddDto.Adapt(appUser);
                    await appUserRepository.AddAsync(appUser);
                    await unitOfWork.SaveChangesAsync();

                    await cacheService.SetCacheAsync($"AppUser_{appUser.Id}", appUser, TimeSpan.FromSeconds(30));
                    result = new SuccessDataResult<AppUserDto>(appUser.Adapt<AppUserDto>(), stringLocalizer[Messages.AppUser_HasBeen_Added]);
                }
                catch (Exception exception)
                {
                    logger.LogError(exception.Message);
                    result = new ErrorDataResult<AppUserDto>(stringLocalizer[Messages.AppUser_CouldNotBe_Added]);
                    transactionScope.Rollback();
                }
                finally
                {
                    transactionScope.Dispose();
                }
            });

            return result;
        }
    }
}