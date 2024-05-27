namespace PoverkaEZ;
using MongoDB.Bson;
using MongoDB.Driver;
using PoverkaEZ.Data;

public class MD
{
    private string connection = "mongodb://localhost:27017";
    private IMongoDatabase database;
    private IMongoCollection<User> userCollection;
    private IMongoCollection<Request> requestCollection;
    public MD()
    {
        var client = new MongoClient(connection);
        database = client.GetDatabase("PoverkaKZN");
        userCollection = database.GetCollection<User>("User");
        requestCollection = database.GetCollection<Request>("Request");
    }
    public void AddUser(User user)
    {
        var collection = database.GetCollection<User>("User");
        collection.InsertOne(user);
    }
    public void AddURequest(Request request)
    {
        var collection = database.GetCollection<Request>("Request");
        collection.InsertOne(request);
    }
    public User FindUserByLogin(string Username)
    {
        var collection = database.GetCollection<User>("User");
        return userCollection.Find(x => x.Username == Username).FirstOrDefault();
    }

    public List<Request> FindAllRequests()
    {
        var collection = database.GetCollection<Request>("Request");
        var requests = collection.Find(x => true).ToList();
        var validRequests = new List<Request>();

        foreach (var request in requests)
        {
            if (request.user == null || string.IsNullOrEmpty(request.user.Username))
            {
                continue; // Skip requests without user information
            }

            var user = FindUserByLogin(request.user.Username);
            if (user != null)
            {
                request.user.Name = user.Name;
                request.user.Telegram = user.Telegram;
                request.user.Phone = user.Phone;
                request.user.Address = user.Address;
                validRequests.Add(request);
            }
        }

        return validRequests;
    }
     public  void ReplaceUser(string username, User user)
        {
            var collection = database.GetCollection<User>("User");
            collection.ReplaceOne(x => x.Username == username, user);
        }
    public List<Request> FindRequestsByUser(string username)
    {
        var collection = database.GetCollection<Request>("Request");
        var requests = collection.Find(r => r.user.Username == username).ToList();
        return requests;
    }
    public void AddReview(Feedback review)
    {
        var collection = database.GetCollection<Feedback>("Feedback");
        collection.InsertOne(review);
    }
    public List<Feedback> GetAllReviews()
    {
        var collection = database.GetCollection<Feedback>("Feedback");
        return collection.Find(x => true).ToList();
    }
}
