namespace Section1SolidPrinciple.SolutionwithoutInterfaceSegregationPrinciple
{
    public class BasicPrinter : IPrintable
    {
        public string Print(string text) => $"printing {text}...";
        public string Scan(string text)
        {
            throw new NotImplementedException();
        }
        public string Fax(string text)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPrintable
    {
        string Print(string text);
        string Scan(string text);
        string Fax(string text);
    }
}