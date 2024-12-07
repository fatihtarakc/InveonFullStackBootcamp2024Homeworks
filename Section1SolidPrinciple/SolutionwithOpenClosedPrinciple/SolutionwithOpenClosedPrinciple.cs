namespace Section1SolidPrinciple.SolutionwithOpenClosedPrinciple
{
    public class ProcessClass
    {
        public void DoProcess(Process process)
        {
            process.Process();
        }
    }

    public class AProcess : Process
    {
        public void Process() => Console.WriteLine("A Process");
    }
    public class BProcess : Process
    {
        public void Process() => Console.WriteLine("B Process");
    }
    public class CProcess : Process
    {
        public void Process() => Console.WriteLine("C Process");
    }
    public interface Process
    {
        void Process();
    }
}