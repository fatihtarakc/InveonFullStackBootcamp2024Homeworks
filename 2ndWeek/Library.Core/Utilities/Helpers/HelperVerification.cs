namespace Library.Core.Utilities.Helpers
{
    public static class HelperVerification
    {
        private static char[] digits = "0123456789".ToCharArray();
        public static string CodeGenerator(int length = 6)
        {
            var random = new Random();
            var verificationCode = new StringBuilder();
            for (int i = 0; i < length; i++) verificationCode.Append(digits[random.Next(0, digits.Length)]);

            return new string(verificationCode.ToString().OrderBy(orderBy => random.Next()).ToArray());
        }
    }
}