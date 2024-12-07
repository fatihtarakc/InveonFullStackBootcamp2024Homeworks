namespace Section1SolidPrinciple.SolutionwithoutDependencyInversionPrinciple
{
    //  Main method
    //  DataAccess dataAccess = new DataAccess();
    //  dataAccess.Save("some data");

    public class DataAccess
    {
        private readonly SqlDatabase sqlDatabase;
        public DataAccess()
        {
            sqlDatabase = new SqlDatabase();
        }

        public void Save(string data) => sqlDatabase.SaveData(data);
    }

    public class SqlDatabase
    {
        public string SaveData(string data) => $"Saving {data}...";
    }
}