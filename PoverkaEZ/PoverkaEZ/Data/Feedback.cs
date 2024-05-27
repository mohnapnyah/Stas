using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.Identity.Client;

namespace PoverkaEZ.Data
{
    public class Feedback
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        ObjectId _id;

        public string fb;
        public User user;

        public Feedback(string fb, User user)
        {
            this.fb = fb;
            this.user = user;
        }
    }
    
}
