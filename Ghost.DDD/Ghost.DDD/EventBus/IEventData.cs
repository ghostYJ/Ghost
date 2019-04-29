using System;

namespace Ghost.DDD
{
    /// <summary>
    /// 事件源接口
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 事件源发生时间
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// 事件源对象
        /// </summary>
        object EventSource { get; set; }
    }
}
