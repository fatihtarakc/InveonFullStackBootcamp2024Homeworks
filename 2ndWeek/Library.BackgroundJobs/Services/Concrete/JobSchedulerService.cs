namespace Library.BackgroundJobs.Services.Concrete
{
    public class JobSchedulerService : IJobSchedulerService
    {
        public void ScheduleJobs() =>
            Hangfire.BackgroundJob.Enqueue(() => FireAndForgetJobs.SendEmailJob());
    }
}