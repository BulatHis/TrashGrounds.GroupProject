using StackExchange.Redis.Extensions.Core.Abstractions;
using TrashGrounds.User.Database.Redis.Interfaces;

namespace TrashGrounds.User.Database.Redis;

public class RedisStore<T> : IRedisStore<T>
{
    private readonly IRedisDatabase _redisDatabase;
    private readonly string _keyPrefix = $"{typeof(T).Name}_";

    public RedisStore(IRedisClient redisClient)
    {
        _redisDatabase = redisClient.Db1;
    }

    public Task<T?> GetAsync(string key)
        => _redisDatabase.GetAsync<T>(GetKey(key));
    
    public Task SetAsync(string key, T value, TimeSpan expiry)
        => _redisDatabase.AddAsync<T>(GetKey(key), value, expiry);

    public Task SetAsync(string key, T value)
        => _redisDatabase.AddAsync<T>(GetKey(key), value);

    public Task DeleteAsync(string key)
        => _redisDatabase.RemoveAsync(GetKey(key));

    public Task<bool> ExistsAsync(string key)
        => _redisDatabase.ExistsAsync(GetKey(key));

    private string GetKey(string key)
        => $"{_keyPrefix}{key}";
}