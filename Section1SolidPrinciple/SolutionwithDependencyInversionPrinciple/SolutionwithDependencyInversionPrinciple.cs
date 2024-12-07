namespace Section1SolidPrinciple.SolutionwithDependencyInversionPrinciple
{
    //  Main method for sql database
    //  IDatabase database = new SqlDatabase();
    //  DataAccess dataAccessWithSqlDatabase = new DataAccess(database);
    //  dataAccessWithSqlDatabase.Save("some data");

    //  Main method for mongodb
    //  IDatabase database = new MongoDb();
    //  DataAccess dataAccessWithMongoDb = new DataAccess(database);
    //  dataAccessWithMongoDb.Save("some data");

    public class DataAccess
    {
        private readonly IDatabase database;
        public DataAccess(IDatabase database)
        {
            this.database = database;
        }

        public void Save(string data) => database.SaveData(data);
    }

    public class SqlDatabase : IDatabase
    {
        public string SaveData(string data) => $"Saving {data} to sql database.";
    }
    public class MongoDb : IDatabase 
    {
        public string SaveData(string data) => $"Saving {data} to mongodb database.";
    }
    public interface IDatabase
    {
        string SaveData(string data);
    }
}