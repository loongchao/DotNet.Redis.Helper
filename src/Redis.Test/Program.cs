using System;
using System.Collections.Generic;
using System.Linq;

using Redis.Helper;

namespace Redis.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisProvider d = new RedisProvider();
            var redis = d.GetDatabase();

            User us = new User() { Id = 10000, Name = "loong", Password = "00000000000000000" };
            redis.JsonHashSet("User", "10000", us);

            us = new User() { Id = 10001, Name = "loong2", Password = "00000000000000000" };
            redis.JsonHashSet("User", "10001", us);
            
            us = redis.JsonHashGet<User>("User", "10000");

            ///////////////////////////////////////////////////////////////////////////////////

            string key = "Token:10000";
            redis.JsonSet(key, "0000000000000000", null);
            key = "Token:10001";
            redis.JsonSet(key, "0000000000000000", null);
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}