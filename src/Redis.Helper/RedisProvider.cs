using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Net;

namespace Redis.Helper
{
    /// <summary>
    /// RedisProvider
    /// </summary>
    public sealed class RedisProvider : IRedisProvider, IDisposable
    {
        #region Connetion Pool

        // 并发控制
        private static readonly object _sync = new object();

        private ConnectionMultiplexer _pool;

        /// <summary>
        /// 获取连接
        /// </summary>
        private ConnectionMultiplexer Pool
        {
            get
            {
                if (_pool == null || !_pool.IsConnected)
                {
                    lock (_sync)
                    {
                        if (_pool == null)
                        {
                            Connect();
                        }
                    }
                }

                return _pool;
            }
        }

        #endregion

        /// <summary>
        /// Options 
        /// </summary>
        private RedisOptions RedisOptions { get; set; }

        /// <summary>
        /// RedisProvider
        /// </summary>
        /// <param name="redisOptions"></param>
        public RedisProvider(RedisOptions redisOptions)
        {
            RedisOptions = redisOptions;
        }

        public RedisProvider()
        {
            RedisOptions = new RedisOptions()
            {
                EndPoints = new string[] { "test-db.paiming.net", "6379" },
                Password = "eims123456",
                DbIndex = 0
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (Pool != null)
            {
                Pool.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IDatabase GetDatabase(int? db = -1)
        {
            return Pool.GetDatabase(db ?? -1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public IServer GetServer(EndPoint endPoint = null)
        {
            if (endPoint == null)
            {
                endPoint = Pool.GetEndPoints().First();
            }

            return Pool.GetServer(endPoint);
        }

        private void Connect()
        {
            var configuration = new ConfigurationOptions
            {
                Password = RedisOptions.Password,
                DefaultDatabase = RedisOptions.DbIndex
            };

            foreach (string endPoint in RedisOptions.EndPoints)
            {
                configuration.EndPoints.Add(EndPointCollection.TryParse(endPoint));
            }

            _pool = ConnectionMultiplexer.Connect(configuration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RedisOptions GetOptions()
        {
            return RedisOptions;
        }
    }
}
