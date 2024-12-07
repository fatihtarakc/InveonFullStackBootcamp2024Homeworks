namespace Section1SolidPrinciple.SolutionwithLiskovSubstitutionPrinciple
{
    public class SmartPhone : Phone, IPlayable
    {
        public string Play(string music) => $"Play {music}";
    }

    public class ClassicPhone : Phone
    {
    }

    public abstract class Phone
    {
        public string Call(string phoneNumber) => $"Calling {phoneNumber}...";
    }
    public interface IPlayable
    {
        string Play(string music);
    }
}