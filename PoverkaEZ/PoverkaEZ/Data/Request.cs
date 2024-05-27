namespace PoverkaEZ.Data;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


public class Request
{
    [BsonId]
    [BsonIgnoreIfDefault]
    ObjectId _id;

    public Request( DateTime dateTime, User user, string status)
    {
        this.dateTime = dateTime;
        this.user = user;
        this.status = status;
    }

    public DateTime dateTime { get; set; }
    public User user { get; set; }
    public string status { get; set; }
}
