using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ghost.DDD
{
    /// <summary>
    /// 事件总线接口
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 注册指定的事件处理器
        /// </summary>
        /// <param name="eventType">事件源类型</param>
        /// <param name="handlerType">事件处理类型</param>
        void Register(Type eventType, Type handlerType);

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        /// <typeparam name="TEventData">事件源泛型</typeparam>
        /// <param name="eventHandler">事件处理</param>
        void Register<TEventData>(IEventHandler eventHandler);

        /// <summary>
        /// 注册Action事件处理器(Action委托)
        /// </summary>
        /// <typeparam name="TEventData">事件源泛型</typeparam>
        /// <param name="action">Action委托</param>
        void Register<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        /// 注册指定程序集中实现的事件处理器
        /// </summary>
        /// <param name="assembly">程序集</param>
        void RegisterAllEventHandlerFromAssembly(Assembly assembly);

        /// <summary>
        /// 取消指定事件源的某个已注册事件处理器
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="handlerType"></param>
        void UnRegister<TEventData>(Type handlerType) where TEventData : IEventData;

        /// <summary>
        /// 取消指定事件源的所有已注册事件处理器
        /// </summary>
        /// <typeparam name="TEventData">事件源泛型</typeparam>
        void UnRegisterAll<TEventData>() where TEventData : IEventData;

        /// <summary>
        /// 触发事件处理
        /// </summary>
        /// <typeparam name="TEventData">事件源泛型</typeparam>
        /// <param name="eventData">事件源</param>
        void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// 触发事件处理，指定的事件处理器
        /// </summary>
        /// <typeparam name="TEventData">事件源泛型</typeparam>
        /// <param name="eventHandlerType">事件处理类型</param>
        /// <param name="eventData">事件源</param>
        void Trigger<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// 触发异步事件处理
        /// </summary>
        /// <typeparam name="TEventData">事件源泛型</typeparam>
        /// <param name="eventData">事件源</param>
        /// <returns></returns>
        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// 触发异步事件处理，指定事件处理器
        /// </summary>
        /// <typeparam name="TEventData">事件源泛型</typeparam>
        /// <param name="eventHandlerType">事件处理类型</param>
        /// <param name="eventData">事件源</param>
        /// <returns></returns>
        Task TriggerAsycn<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData;
    }
}
