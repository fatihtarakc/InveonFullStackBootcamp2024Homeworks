namespace Library.Core.Utilities.Helpers
{
    public static class HelperPassword
    {
        private static char[] upperCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static char[] lowerCharacters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static char[] digits = "0123456789".ToCharArray();
        private static char[] symbols = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '[', ']', '{', '}', '\\', '|', ';', ':', '\'', '\"', ',', '.', '<', '>', '/', '?', '`', '~' };
        public static string Generator(int length = 8)
        {
            var random = new Random();
            var password = new StringBuilder();

            password.Append(upperCharacters[random.Next(upperCharacters.Length)]);
            password.Append(lowerCharacters[random.Next(lowerCharacters.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(symbols[random.Next(symbols.Length)]);

            char[] allCharacters = upperCharacters.Concat(lowerCharacters).Concat(digits).Concat(symbols).ToArray();

            allCharacters = allCharacters.OrderBy(orderBy => random.Next(allCharacters.Length)).ToArray();

            for (int i = 5; i <= length; i++)
            {
                password.Append(allCharacters[random.Next(allCharacters.Length)]);
            }

            return new string(password.ToString().OrderBy(orderBy => random.Next(allCharacters.Length)).ToArray());
        }
    }
}