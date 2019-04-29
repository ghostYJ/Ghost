using System;

namespace Ghost.DDD
{
    public class EventData : IEventData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EventData()
        {
            EventTime = DateTime.Now;
        }

        /// <summary>
        /// 事件发生时间
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 事件源对象
        /// </summary>
        public object EventSource { get; set; }
    }
}
