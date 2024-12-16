namespace Library.Dtos.EmailDtos
{
    public class EmailForVerificationCodeDto
    {
        public string To { get; set; }
        public string EmailTo { get; set; }
        public string VerificationCode { get; set; }
    }
}