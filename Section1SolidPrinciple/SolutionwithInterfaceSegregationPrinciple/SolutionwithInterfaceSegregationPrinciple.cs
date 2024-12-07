namespace Section1SolidPrinciple.SolutionwithInterfaceSegregationPrinciple
{
    public class AllInOnePrinter : IPrintable, IScanable, IFaxable
    {
        public string Print(string text) => $"printing {text}...";
        public string Scan(string text) => $"scanning {text}...";
        public string Fax(string text) => $"sending fax {text}...";
    }

    public class BasicPrinter : IPrintable
    {
        public string Print(string text) => $"printing {text}...";
    }

    public interface IPrintable
    {
        string Print(string text);
    }
    public interface IScanable
    {
        string Scan(string text);
    }
    public interface IFaxable
    {
        string Fax(string text);
    }
}