using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PoverkaEZ.Data;

namespace PoverkaEZ
{

    public class Brigade
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;

        public string name;
        public User brigadeMember;
        public User secondBrigadeMember;
        public List<Request> brigadeWork;
        public Brigade(string name, User brigadeMember, User secondBrigadeMember, List<Request> brigadeWork)
        {
            this.name = name;
            this.brigadeMember = brigadeMember;
            this.secondBrigadeMember = secondBrigadeMember;
            this.brigadeWork = brigadeWork;
        }
    }
}
