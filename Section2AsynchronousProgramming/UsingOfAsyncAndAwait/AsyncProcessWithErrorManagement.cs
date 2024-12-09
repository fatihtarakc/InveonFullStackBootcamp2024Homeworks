namespace Section2AsynchronousProgramming.UsingOfAsyncAndAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                string filePath = "example.txt";
                string content = await AsyncProcessWithErrorManagement.ReadFileAsync(filePath);
                Console.WriteLine($"File content \n ----------------------------------- \n{content}");
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine($"Error: The file was not found. {exception.Message}");
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.WriteLine($"Error: You do not have permission to access this file. {exception.Message}");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An unexpected error occurred: {exception.Message}");
            }
        }
    }

    public class AsyncProcessWithErrorManagement
    {
        public static async Task<string> ReadFileAsync(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}