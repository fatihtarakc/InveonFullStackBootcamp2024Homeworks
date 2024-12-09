namespace Section2AsynchronousProgramming.AsynchronousMethodWriting
{
    class AllStaticMethodsOfTaskClass
    {
        static async Task Main(string[] args)
        {
            // Task.Delay()
            // It is often used when there is a need for a certain amount of waiting time between asynchronous tasks, allowing the application to run more efficiently.
            Console.WriteLine("Delay started...");
            await Task.Delay(3000); // During 3 seconds, the main thread is not blocked thanks to await keyword.
            Console.WriteLine("Delay completed. Continuing with the next operation.");

            // Task.FromCanceled()
            // The Task.FromCanceled() method is used to create a Task object that is immediately canceled. This method is typically used in scenarios where a cancellation token is used and represents tasks that have been canceled.
            var cancellationTokenSource = new CancellationTokenSource();
            var cancelledTask = Task.FromCanceled(cancellationTokenSource.Token);
            try
            {
                cancelledTask.Wait();
            }
            catch
            {
                Console.WriteLine("Task was canceled.");
            }

            // Task.FromException()
            // The Task.FromException() method is used to create a Task object that completes immediately with a specific exception. This method is especially useful for simulating error situations or for testing error conditions.
            var faultedTask = Task.FromException(new InvalidOperationException("An error occurred."));
            try
            {
                faultedTask.Wait();
            }
            catch (AggregateException exception)
            {
                Console.WriteLine("Task encountered an exception: " + exception.InnerException.Message);
            }

            // Task.FromResult()
            // The Task.FromResult() method is used to create an immediately completed Task object and returns a specific value. This method is especially useful for ensuring that asynchronous methods return a result synchronously.
            Task<int> resultTask = Task.FromResult(42);
            int result = await resultTask;
            Console.WriteLine("The result is: " + result);

            // Task.Run()
            var taskRun = Task.Run(() =>
            {
                // long running process...
                Console.WriteLine("The task began...");
                Thread.Sleep(10000);
            });
            taskRun.Wait(); // Wait for the taskRun to completed
            Console.WriteLine("The task was completed !");

            // Task.WaitAll()
            // The Task.WaitAll() method is used to wait for more than one Task object to complete. This method synchronously waits for all specified tasks to be completed and continues to progress only after they have all been completed. This is useful in situations where the main thread needs to be waited until all of the tasks that are running in parallel are completed.
            var taskWaitAll1 = Task.Run(() =>
            {
                Console.WriteLine("taskWaitAll1 started..."); 
                Thread.Sleep(2000); // 2 seconds delay
                Console.WriteLine("taskWaitAll1 completed.");
            });
            var taskWaitAll2 = Task.Run(() =>
            {
                Console.WriteLine("taskWaitAll2 started...");
                Thread.Sleep(3000); // 3 seconds delay
                Console.WriteLine("taskWaitAll2 completed.");
            });
            Task.WaitAll(taskWaitAll1, taskWaitAll2);
            Console.WriteLine("All tasks completed.");

            // Task.WaitAny()
            // The Task.WaitAny() method allows you to continue when any of these tasks are completed. That is, it waits for the first one to be completed from the given tasks and then proceeds to move forward. This method is very useful in scenarios where you want to continue working with the first completed operation, especially while waiting for the result of multiple asynchronous operations.
            var taskWaitAny1 = Task.Run(() =>
            {
                Console.WriteLine("taskWaitAny1 started...");
                Thread.Sleep(2000); // 2 seconds delay
                Console.WriteLine("taskWaitAny1 completed.");
            });
            var taskWaitAny2 = Task.Run(() =>
            {
                Console.WriteLine("taskWaitAny2 started...");
                Thread.Sleep(3000); // 3 seconds delay
                Console.WriteLine("taskWaitAny2 completed.");
            });
            var completedTaskIndex = Task.WaitAny(taskWaitAny1, taskWaitAny2); 
            Console.WriteLine($"taskWaitAny{completedTaskIndex + 1} completed first.");

            // Task.WhenAll()
            // The Task.WhenAll() method is used to wait for multiple Task objects to complete and continue when they are all complete. This method synchronously waits for all specified tasks to be completed. And once all is completed, it continues to move forward. This method is useful when the main thread needs to be waited until all of the tasks that are running in parallel are completed.
            var taskWhenAll1 = Task.Run(() =>
            {
                Console.WriteLine("taskWhenAll1 started...");
                Thread.Sleep(2000); // 2 seconds delay
                Console.WriteLine("taskWhenAll1 completed.");
            });
            var taskWhenAll2 = Task.Run(() =>
            {
                Console.WriteLine("taskWhenAll2 started...");
                Thread.Sleep(3000); // 3 seconds delay
                Console.WriteLine("taskWhenAll2 completed.");
            });
            await Task.WhenAll(taskWhenAll1, taskWhenAll2);
            Console.WriteLine("All tasks completed.");

            // Task.WhenAny()
            // The Task.WhenAny() method allows you to continue when any of these tasks are completed. That is, it waits for the first one to be completed from the given tasks and then proceeds to move forward. It is very useful, especially in scenarios where you want to continue working with the first completed operation, while waiting for the result of multiple asynchronous operations.
            var taskWhenAny1 = Task.Run(() =>
            {
                Console.WriteLine("taskWhenAny1 started...");
                Thread.Sleep(2000); // 2 seconds delay
                Console.WriteLine("taskWhenAny1 completed.");
            });
            var taskWhenAny2 = Task.Run(() =>
            {
                Console.WriteLine("taskWhenAny2 started...");
                Thread.Sleep(3000); // 3 seconds delay
                Console.WriteLine("taskWhenAny2 completed.");
            });
            var firstCompletedTask = await Task.WhenAny(taskWhenAny1, taskWhenAny2);
            Console.WriteLine("A task has completed.");
            Console.WriteLine($"First completed task status: {firstCompletedTask.Status}");
        }
    }
}