using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Ghost.DDD
{
    /// <summary>
    /// Ioc容器
    /// </summary>
    public class IocContainer
    {
        private IWindsorContainer _container;
        private static IocContainer _instance = null;
        private static readonly object _lock = new object();

        //1.注册相关实现类到特定的安装类2.执行安装类的安装。

        private IocContainer()
        {
            //1、容器创建时，可以将xml配置文件传递给引用。
            //IResource resource = new AssemblyResource("Castle.Services.config");
            //_container = new WindsorContainer(new XmlInterpreter(resource));
            //2、可以使用无参数的构造函数的XML解释器，AppDomain的配置文件将作为源的配置：(更灵活)
            //_container = new WindsorContainer(new XmlInterpreter());

            //
            _container = new WindsorContainer()
                .Install(
                Configuration.FromAppConfig(),
                    FromAssembly.This()
                );
        }

        /// <summary>
        /// 注册Component
        /// </summary>
        private static IocContainer Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new IocContainer();
                    return _instance;
                }
            }
        }

        /// <summary>
        /// 取IoC容器
        /// </summary>
        /// <returns></returns>
        public static IWindsorContainer GetContainer()
        {
            return Instance._container;
        }

        /// <summary>
        /// 从IoC中取注册实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="componentId">组件ID默认为空（注意：正常情况不推荐使用，增加耦合性，违反解耦。）</param>
        /// <returns></returns>
        public static T Get<T>(string componentId = "")
        {
            if (componentId == string.Empty)
                return (T)GetContainer()[typeof(T)];//谁先注册，调用谁
            return (T)GetContainer().Resolve<T>(componentId);//根据Id获取注册的组件   (T)GetContainer()[componentId]
        }

        /// <summary>
        /// 是否注册过某个实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasComponent<T>()
        {
            return Instance._container.Kernel.HasComponent(typeof(T));
        }
    }
}
