using System;
using System.Configuration;

namespace Ghost.Utility
{
    /// <summary>
    /// 雪花ID
    /// </summary>
    public class SnowFlake
    {
        #region [^]、[|]、[<<] 运算符注释

        /* 
        * 一、"^" 针对整型类型和 bool 预定义了二元 "^" 运算符。 对于整型类型，"^" 会计算其操作数的按位异或。 对于 bool 操作数，"^" 计算其操作数的逻辑异或；即，当且仅当其一个操作数为 true 时，结果才为 true。
        * PS: 0^0=0; 0^1=1; 1^0=1; 1^1=0;
        * 
        * 二、"|" 对于整型类型，"|" 会计算其操作数的按位 OR。 对于 bool 操作数，"|" 会计算其操作数的逻辑 OR；即，当且仅当其两个操作数皆为 false 时，结果才为 false。
        * PS: 0|0=0; 0|1=1; 1|0=1; 1|1=1;
        * 
        * "<<" 左移运算符 "<<" 将第一个操作数向左移动第二个操作数指定的位数。 第二个操作数的类型必须为 int 或预定义隐式数值转换为 int 的类型。
        * PS: 8<<4=128(1乘以2的7次方)
        */

        #endregion

        /// <summary>
        /// 加锁对象
        /// </summary>
        private static object syncRoot = new object();

        /// <summary>
        /// 
        /// </summary>
        private static SnowFlake snowflake = null;

        #region 机器、数据、随机量字节

        /// <summary>
        /// 机器Id
        /// </summary>
        private static long machineId;

        /// <summary>
        /// 数据Id
        /// </summary>
        private static long datacenterId = 0L;

        /// <summary>
        /// 计数从零开始
        /// </summary>
        private static long sequence = 0L;

        /// <summary>
        /// 唯一时间随机量（当前起始时间戳）
        /// </summary>
        private static long twepoch = Convert.ToInt64(ConfigurationManager.AppSettings["SFID_Twepoch"]);

        /// <summary>
        /// 机器码字节数
        /// </summary>
        private static long machineIdBits = 5L;

        /// <summary>
        /// 数据字节数
        /// </summary>
        private static long datacenterIdBits = 5L;

        /// <summary>
        /// 最大机器ID
        /// </summary>
        public static long maxMachineId = -1L ^ -1L << (int)machineIdBits;

        /// <summary>
        /// 最大数据ID
        /// </summary>
        private static long maxDatacenterId = -1L ^ (-1L << (int)datacenterIdBits);

        /// <summary>
        /// 计数器字节数，12个字节用来保存计数码
        /// </summary>
        private static long sequenceBits = 12L;

        /// <summary>
        /// 机器码数据左移位数，计时后面计数器占用的位数
        /// </summary>
        private static long machineIdShift = sequenceBits;

        /// <summary>
        /// 数据字节数据左移位数=机器码+计数器总字节数
        /// </summary>
        private static long datacenterIdShift = sequenceBits + machineIdBits;

        /// <summary>
        /// 时间戳左移位数=机器码+计数器总字节数+数据字节数
        /// </summary>
        private static long timestampLeftShift = sequenceBits + machineIdBits + datacenterIdBits;

        /// <summary>
        /// 一微秒内可以产生计数，如果达到该值则等到下一微秒再进行生成
        /// </summary>
        public static long sequenceMask = -1L ^ -1L << (int)sequenceBits;

        /// <summary>
        /// 最后时间戳
        /// </summary>
        private static long lastTimestamp = -1L;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        private static SnowFlake Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (snowflake == null)
                        snowflake = new SnowFlake();
                    return snowflake;
                }
            }

        }

        /// <summary>
        /// 雪花ID
        /// </summary>
        public SnowFlake()
        {
            Instance.SnowFlakes(0L, -1);
        }

        /// <summary>
        /// 雪花ID(分布式，非多线程使用)
        /// </summary>
        /// <param name="machineId">机器码ID</param>
        public SnowFlake(long machineId)
        {
            Instance.SnowFlakes(machineId, -1);
        }

        /// <summary>
        /// 雪花ID(分布式，多线程使用)
        /// </summary>
        /// <param name="machineId">机器码ID</param>
        /// <param name="datacenterId">数据中心ID</param>
        public SnowFlake(long machineId, long datacenterId)
        {
            Instance.SnowFlakes(machineId, datacenterId);
        }

        /// <summary>
        /// 将机器码ID、数据中心ID赋值
        /// </summary>
        /// <param name="machineId">机器码ID</param>
        /// <param name="datacenterId">数据中心ID</param>
        private void SnowFlakes(long machineId, long datacenterId)
        {
            if (machineId >= 0)
            {
                if (machineId > maxMachineId)
                {
                    throw new Exception("机器码ID非法");
                }
                SnowFlake.machineId = machineId;
            }
            if (datacenterId >= 0)
            {
                if (datacenterId > maxDatacenterId)
                {
                    throw new Exception("数据中心ID非法");
                }
                SnowFlake.datacenterId = datacenterId;
            }
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns></returns>
        private static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1997, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        /// <summary>
        /// 获取下一微秒时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private static long GetNextTimestamp(long lastTimestamp)
        {
            long timestamp = GetTimestamp();
            if (timestamp <= lastTimestamp)
            {
                timestamp = GetTimestamp();
            }
            return timestamp;
        }

        /// <summary>
        /// 获取SnowFlakeID
        /// </summary>
        /// <returns></returns>
        public static long GetNewId()
        {
            lock (syncRoot)
            {
                long timestamp = GetTimestamp();
                if (lastTimestamp == timestamp)
                {
                    //同一微秒内生成ID,用&运算计算该微秒内产生的计数是否已经达到上限。
                    sequence = (sequence + 1) & sequenceMask;
                    if (sequence == 0)
                    {
                        timestamp = GetNextTimestamp(lastTimestamp);
                    }
                }
                else
                {
                    //不同微秒生成ID
                    sequence = 0L;
                }
                if (timestamp < lastTimestamp)
                {
                    throw new Exception("时间戳小于上一次生成ID的时间戳。");
                }
                //把当前时间戳保存为最后生成ID的时间戳
                lastTimestamp = timestamp;
                long Id = ((timestamp - twepoch) << (int)timestampLeftShift) | (datacenterId << (int)datacenterIdShift) | (machineId << (int)machineIdShift) | sequence;
                return Id;
            }
        }
    }
}
