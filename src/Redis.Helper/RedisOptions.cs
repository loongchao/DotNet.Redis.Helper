using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Helper
{
    /// <summary>
    /// Redis Options
    /// </summary>
    public class RedisOptions
    {
        /// <summary>
        /// Redis end points, such as "{host or ip}:{port}"
        /// </summary>
        public string[] EndPoints { get; set; }

        /// <summary>
        /// Redis password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Default redis database
        /// </summary>
        public int DbIndex { get; set; }
    }
}
