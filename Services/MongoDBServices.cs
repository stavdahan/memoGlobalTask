using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services;

public class MongoDBService
{

    private readonly IMongoCollection<User> _usersCollection;

    public MongoDBService(IOptions<MongoDBSetting> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _usersCollection = database.GetCollection<User>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(User user)
    {
        await _usersCollection.InsertOneAsync(user);
        return;
    }

    public async Task<List<User>> GetAsync(int page)
    {
        int pageSize = 6;
        int start = (page - 1) * pageSize;
        return await _usersCollection.Find(_ => true).Skip(start).Limit(pageSize).ToListAsync();
    }

    public async Task<User> GetUserAsync(int id)
    {
        return await _usersCollection.Find(u => u.id == id).FirstOrDefaultAsync();
    }

    public async Task<User> EditUserAsync(int id, User user)
    {
        User updatedUser = await _usersCollection.Find(u => u.id == id).FirstOrDefaultAsync();
        if (updatedUser == null) {
            Console.WriteLine("hi");
            return updatedUser;
        } 
        updatedUser = await _usersCollection.FindOneAndUpdateAsync(
        Builders<User>.Filter.Where(u => u.id == id),
        Builders<User>.Update
        .Set(u => u.email, user.email)
        .Set(u => u.first_name, user.first_name)
        .Set(u => u.last_name, user.last_name)
        .Set(u => u.avatar, user.avatar));
        return user;
    }

    public async Task<User> DeleteUserAsync(int id)
    {
        User deletedUser = await _usersCollection.Find(u => u.id == id).FirstOrDefaultAsync();
        if (deletedUser == null) {
            return deletedUser;
        } 
        deletedUser = await _usersCollection.FindOneAndDeleteAsync(
            Builders<User>.Filter.Where(u => u.id == id));
        return deletedUser;
    }
}