using MongoDB.Driver;

namespace Cat.M.Log.Services
{
    public class AppDbContext
    {
        public MongoClient client;
        public AppDbContext()
        {
            client = new MongoClient(Cat.Foundation.ConfigManager.ConnectionStrings.MongoDB.ConnectionStrings);
        }
    }
}
