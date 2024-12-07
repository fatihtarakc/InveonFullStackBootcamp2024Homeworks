namespace Section1SolidPrinciple.SolutionwithoutOpenClosedPrinciple
{
    public class ProcessClass
    {
        public ProcessClass()
        {
            AProcess = new AProcess();
            BProcess = new BProcess();
            CProcess = new CProcess();
        }

        public AProcess AProcess { get; set; }
        public BProcess BProcess { get; set; }
        public CProcess CProcess { get; set; }

        public void DoProcess(TypeOfProcess typeOfProcess)
        {
            switch (typeOfProcess)
            {
                case TypeOfProcess.AProcess: AProcess.Process(); break;
                case TypeOfProcess.BProcess: BProcess.Process(); break;
                case TypeOfProcess.CProcess: CProcess.Process(); break;
            }
        }
    }

    public enum TypeOfProcess
    {
        AProcess,
        BProcess,
        CProcess
    }
    public class AProcess
    {
        public void Process() => Console.WriteLine("A Process");
    }
    public class BProcess
    {
        public void Process() => Console.WriteLine("B Process");
    }
    public class CProcess
    {
        public void Process() => Console.WriteLine("C Process");
    }
}