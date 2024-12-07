namespace Section1SolidPrinciple.SolutionwithoutLiskovSubstitutionPrinciple
{
    public class SmartPhone : Phone
    {
        public string Call(string phoneNumber) => $"Calling {phoneNumber}...";
        public override string Play(string music) => $"Play {music}";
    }

    public class ClassicPhone : Phone
    {
        public string Call(string phoneNumber) => $"Calling {phoneNumber}...";

        public override string Play(string music)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Phone
    {
        public string Call(string phoneNumber) => $"Calling {phoneNumber}...";
        public abstract string Play(string music);
    }
}