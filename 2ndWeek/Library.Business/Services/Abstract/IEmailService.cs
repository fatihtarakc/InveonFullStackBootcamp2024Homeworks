namespace Library.Business.Services.Abstract
{
    public interface IEmailService
    {
        Task SendAsync(EmailDto emailDto);
    }
}