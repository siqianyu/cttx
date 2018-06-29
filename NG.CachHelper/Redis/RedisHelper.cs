using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;

namespace NG.CachHelper.Redis
{
    /// <summary>
    /// 基于Redis服务的缓存工具类
    /// </summary>
    public class RedisHelper
    {


        RedisClient client = new RedisClient("127.0.0.1", 6379);

        /// <summary>
        /// 获取当前客户端连接对象
        /// </summary>
        /// <returns></returns>
        public RedisClient GetClient()
        {
            return client;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            client.Dispose();
            client.Quit();
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ExistsKey(string key)
        {
            return client.Exists(key) == 0 ? false : true;
        }

        public byte[][] ListKeys(string pattern)
        {
            return client.Keys(pattern);
        }

        /// <summary>
        /// 设置字符串类型的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetStringCash(string key, string value)
        {
            return client.Set<string>(key, value);
        }


        /// <summary>
        /// 获取字符串类型的缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetStringCash(string key)
        {
            string value = client.Get<string>(key);
            return value == null ? "" : value;
        }


        /// <summary>
        /// 设置hash表类型的缓存值
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetHashCash(string hashId,string key, string value)
        {
            return client.SetEntryInHash(hashId, key, value);
        }

        /// <summary>
        /// 获取集合数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="ifkey"></param>
        /// <returns></returns>
        public List<string> GetHashCash(string hashId, bool ifkey)
        {
            if (ifkey)
            {
                return client.GetHashKeys(hashId);
            }
            else
            {
                return client.GetHashValues(hashId);
            }
        }      
    }
}
