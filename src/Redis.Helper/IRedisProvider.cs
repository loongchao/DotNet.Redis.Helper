using StackExchange.Redis;
using System.Net;

namespace Redis.Helper
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IRedisProvider
    {
        /// <summary>
        /// Get the redis database instance
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        IDatabase GetDatabase(int? db = -1);

        /// <summary>
        /// Get the redis server
        /// </summary>
        /// <param name="endPoint">The specical end point. Return the first server while passing null</param>
        /// <returns></returns>
        IServer GetServer(EndPoint endPoint = null);

        /// <summary>
        /// Options
        /// </summary>
        /// <returns></returns>
        RedisOptions GetOptions();
    }
}
