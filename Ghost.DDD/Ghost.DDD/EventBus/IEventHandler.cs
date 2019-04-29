namespace Ghost.DDD
{
    /// <summary>
    /// 事件处理接口
    /// </summary>
    public interface IEventHandler
    {
    }

    /// <summary>
    /// 事件处理泛型接口
    /// </summary>
    /// <typeparam name="TEventData"></typeparam>
    public interface IEventHandler<TEventData> : IEventHandler where TEventData : IEventData
    {
        /// <summary>
        /// 事件处理方法
        /// </summary>
        /// <param name="eventData"></param>
        void HandlerEvent(TEventData eventData);
    }
}
