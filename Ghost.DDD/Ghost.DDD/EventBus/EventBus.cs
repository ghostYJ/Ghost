using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Ghost.DDD
{
    /// <summary>
    /// 事件总线
    /// 个人理解：
    /// 1.首先调用事件总线的注册方法，将所有继承于IEventHandler<TEventData>泛型接口的实现类都注册到Ioc容器中，
    /// 再将它们注册到事件源对应事件处理器字典，线程安全集合中
    /// 2.按照不同的事件源类型(fish,cat,dog)触发事件总线的事件处理方法,一致触发。
    /// 举例:事件源为Fish的实现类的事件处理方法
    /// public class FinshHandler:IEventHandler<Fish>{public void HandlerEvent(){......} }，这种类会存在多个
    /// 下面的handlerType就是FinshHandler这种继承了泛型接口的事件处理器(实现类)
    /// 以下类实现了多种不同的触发方式和注入方式，但是总体的思想都是一致的。
    /// </summary>
    public class EventBus : IEventBus
    {
        /// <summary>
        /// 默认事件总线实例，单例模式，确保事件总线的唯一入口
        /// </summary>
        public static EventBus Default => new EventBus();

        //public static EventBus Defalut { get; private set; }

        //static EventBus()
        //{
        //    Defalut = new EventBus();
        //}
        public IWindsorContainer IocContainer { get; private set; }
       
        /// <summary>
        /// 事件源对应事件处理器字典，线程安全集合
        /// </summary>
        private readonly ConcurrentDictionary<Type, List<Type>>
            _eventAndHandlerMapping;

        public static object _lock = new object();

        public EventBus()
        {
            //初始化,不加载本地配置文件
            //IocContainer = new WindsorContainer();

            //加载本地配置文件，初始化Ioc
            IocContainer = DDD.IocContainer.GetContainer();
            _eventAndHandlerMapping = new ConcurrentDictionary<Type, List<Type>>();
        }

        /// <summary>
        /// 事件源对应事件处理器字典中指定事件源的事件处理器列表
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private List<Type> GetOrAddEvnentAndHanlderMapping(Type eventType)
        {
            //1.如果此Key值不存在，则新增一个key为事件源，value为new List<Type>()的键值对放入安全线程ConcurrentDictionary字典
            //2.如果key值存在，则获取key为xxx事件源的安全线程字典
            return _eventAndHandlerMapping.GetOrAdd(eventType, type => new List<Type>());
        }

        /// <summary>
        /// 注册指定的事件处理器
        /// </summary>
        /// <param name="eventType">事件源类型</param>
        /// <param name="handlerType">事件处理类型</param>
        public void Register(Type eventType, Type handlerType)
        {
            //搜索具有指定名称的接口
            var handlerInterface = handlerType.GetInterface("IEventHandler`1");
            //若IocContainer不包含此接口组件，则将此接口注入Ioc
            if (!IocContainer.Kernel.HasComponent(handlerInterface))
                IocContainer.Register(Component.For(handlerInterface, handlerType));
            //找到了key为xxx的事件源，将value为xxx事件处理类型的类存入安全线程字典。
            lock (_lock)
                GetOrAddEvnentAndHanlderMapping(eventType).Add(handlerType);
        }

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        /// <typeparam name="TEventData">事件源类型</typeparam>
        /// <param name="eventHandler">事件处理类型</param>
        public void Register<TEventData>(IEventHandler eventHandler)
        {
            //固定住事件处理类型，必须继承于IEventHandler
            Register(typeof(TEventData), eventHandler.GetType());
        }

        /// <summary>
        /// 注册Action事件处理器(Action委托),Action委托表示引用一个void返回类型的方法
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="action"></param>
        public void Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            //固定住事件源类型，必须继承于IEventData

            //1.构造ActionEventHandler
            var actionHandler = new ActionEventHandler<TEventData>(action
                );

            //2.将ActionEventHandler的实例注入到Ioc容器
            IocContainer.Register(Component.For<IEventHandler<TEventData>>().UsingFactoryMethod(() => actionHandler).LifestyleSingleton());

            //3.注册到事件总线
            Register<TEventData>(actionHandler);
        }

        /// <summary>
        /// 注册指定程序集中实现的事件处理器
        /// </summary>
        /// <param name="assembly"></param>
        public void RegisterAllEventHandlerFromAssembly(Assembly assembly)
        {
            //1.将IEventHandler注册到Ioc容器
            IocContainer.Register(Classes.FromAssembly(assembly)
                .BasedOn(typeof(IEventHandler<>))
                .WithService.AllInterfaces()
                .LifestyleSingleton());

            //2.从IOC容器中获取注册的所有IEventHandler
            var handlers = IocContainer.Kernel.GetHandlers(typeof(IEventHandler));
            foreach (var handler in handlers)
            {
                //循环遍历所有的IEventHandler<T>
                var interfaces = handler.ComponentModel.Implementation.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (!typeof(IEventHandler).IsAssignableFrom(@interface))
                    {
                        continue;
                    }

                    //获取泛型参数类型
                    var genericArgs = @interface.GetGenericArguments();
                    if (genericArgs.Length == 1)
                    {
                        //注册到事件源与事件处理的映射字典中
                        Register(genericArgs[0], handler.ComponentModel.Implementation);
                    }
                }
            }
        }

        /// <summary>
        /// 触发事件处理
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="eventData"></param>
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            //获取所有映射的EventHandler
            List<Type> handlerTypes = _eventAndHandlerMapping[typeof(TEventData)];

            if (handlerTypes != null && handlerTypes.Count > 0)
            {
                //找到泛型接口,当前事件源对应的实现类所继承的泛型接口应该是一致的：例如 IEventHandler<Fish>
                var handlerInterface = handlerTypes.FirstOrDefault().GetInterface("IEventHandler`1");
                //从Ioc容器中获取所有的实例,所有继承于IEventHandler的类的实例
                var eventHandlers = IocContainer.ResolveAll(handlerInterface);
                foreach (var handlerType in handlerTypes)
                {
                    //循环遍历，仅当解析的实例类型与映射字典中事件处理类型一致时，才触发事件
                    foreach (var eventHandler in eventHandlers)
                    {
                        if (eventHandler.GetType() == handlerType)
                        {
                            var handler = eventHandler as IEventHandler<TEventData>;
                            handler?.HandlerEvent(eventData);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 触发事件处理，指定的事件处理器
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="eventHandlerType"></param>
        /// <param name="eventData"></param>
        public void Trigger<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData
        {
            //找到此事件处理对象的泛型接口
            var handlerInterface = eventHandlerType.GetInterface("IEventHandler`1");
            //从IoC容器中获取所有此泛型接口的事件处理器实例
            var eventHandlers = IocContainer.ResolveAll(handlerInterface);
            foreach (var eventHandler in eventHandlers)
            {
                //当类型与当前传入的事件处理器类型一致时，触发事件处理方法
                if (eventHandler.GetType() == eventHandlerType)
                {
                    var handler = eventHandler as IEventHandler<TEventData>;
                    handler?.HandlerEvent(eventData);
                }
            }
        }

        public Task TriggerAsycn<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData
        {
            return Task.Run(() => Trigger<TEventData>(eventHandlerType, eventData));
        }

        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            return Task.Run(() => Trigger<TEventData>(eventData));
        }

        /// <summary>
        /// 取消指定事件源的某个已注册事件处理器
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="handlerType"></param>
        public void UnRegister<TEventData>(Type handlerType) where TEventData : IEventData
        {
            _eventAndHandlerMapping.GetOrAdd(typeof(TEventData), (type) => new List<Type>())
        .RemoveAll(t => t == handlerType);
        }

        /// <summary>
        /// 取消指定事件源的所有已注册事件处理器
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        public void UnRegisterAll<TEventData>() where TEventData : IEventData
        {
            _eventAndHandlerMapping.GetOrAdd(typeof(TEventData), (type) => new List<Type>()).Clear(); ;
        }
    }
}
