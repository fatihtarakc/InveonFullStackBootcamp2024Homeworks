namespace Section2AsynchronousProgramming.AsynchronousMethodWriting
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var syncAndAsyncMethods = new SyncAndAsyncMethods();

            Console.WriteLine("The synchronous method started to work...");
            syncAndAsyncMethods.SyncProcess();
            Console.WriteLine("The synchronous method was completed !");

            Console.WriteLine("The asynchronous method started to work...");
            await syncAndAsyncMethods.AsyncProcess();
            Console.WriteLine("The asynchronous method was completed !");
        }
    }

    public class SyncAndAsyncMethods
    {
        public void SyncProcess()
        {
            Console.WriteLine("The first process began...");
            var process1 = Task.Delay(10000);
            Console.WriteLine("The first process was completed !");

            Console.WriteLine("The second process began...");
            var process2 = Task.Delay(5000);
            Console.WriteLine("The second process was completed !");
        }

        public async Task AsyncProcess()
        {
            Console.WriteLine("The first process began...");
            var process1 = Task.Delay(10000);

            Console.WriteLine("The second process began...");
            var process2 = Task.Delay(5000);

            await Task.WhenAll(process1, process2);
            Console.WriteLine("The first and second processes were completed !");
        }
    }
}