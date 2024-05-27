namespace PoverkaEZ;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PoverkaEZ.Data;

public class User
{
    [BsonId]
    [BsonIgnoreIfDefault]
    ObjectId _id;

    public User(string username, string password, string telegram, string name, string phone, string address)
    {
        Username = username;
        Password = password;
        Telegram = telegram;
        Name = name;
        Phone = phone;
        Address = address;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public string Telegram { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}