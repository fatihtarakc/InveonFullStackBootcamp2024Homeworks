namespace Library.Core.Utilities.Helpers
{
    public static class HelperISBN
    {
        private const string prefix = "978";
        private static readonly Random random = new();
        public static string Generator()
        {
            var registirationGroup = GenerateRandomDigits(1);
            var registant = GenerateRandomDigits(4);
            var publication = GenerateRandomDigits(4);

            string isbnWithoutCalculateWithCheckingDigit = prefix + registirationGroup + registant + publication;
            string calculateWithCheckingDigit = CalculateWithCheckingDigit(isbnWithoutCalculateWithCheckingDigit).ToString();

            return $"{prefix}-{registirationGroup}-{registant}-{publication}-{calculateWithCheckingDigit}";
        }

        private static string GenerateRandomDigits(int length)
        {
            var digits = string.Empty;
            for (int i = 0; i < length; i++) digits += random.Next(0, 10).ToString();

            return digits;
        }

        private static int CalculateWithCheckingDigit(string isbnWithoutCalculateWithCheckingDigit)
        {
            var sum = 0;
            for (int i = 0; i < isbnWithoutCalculateWithCheckingDigit.Length; i++)
            {
                int digit = (int)isbnWithoutCalculateWithCheckingDigit[i];

                if (i % 2 is 0) sum += digit;
                else sum += 3 * digit;
            }
            var remainder = sum % 10;
            return (10 - remainder) % 10;
        }
    }
}