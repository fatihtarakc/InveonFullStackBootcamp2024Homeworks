namespace Library.Business.Abstract.Services.Abstract
{
    public interface IEmailService
    {
        Task SendAsync(EmailDto emailDto);
    }
}