namespace Library.Core.Utilities.Helpers
{
    public static class HelperCharacter
    {
        private static char[] digits = "0123456789".ToCharArray();
        private static char[] turkishCharacters = { 'ç', 'Ç', 'ğ', 'Ğ', 'ı', 'İ', 'ö', 'Ö', 'ş', 'Ş', 'ü', 'Ü' };
        private static char[] symbols = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '[', ']', '{', '}', '\\', '|', ';', ':', '\'', '\"', ',', '.', '<', '>', '/', '?', '`', '~' };
        public static bool DigitControl(string input) =>
            string.IsNullOrWhiteSpace(input) is true ? false : input.Any(character => digits.Contains(character));

        public static bool TRCharacterControl(string input) =>
            string.IsNullOrWhiteSpace(input) is true ? false : input.Any(character => turkishCharacters.Contains(character));

        public static bool SymbolControl(string input) => 
            string.IsNullOrWhiteSpace(input) is true ? false : input.Any(character => symbols.Contains(character));
    }
}