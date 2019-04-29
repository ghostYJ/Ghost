using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost.CRUD.Domain
{
    /// <summary>
    /// Domain扩展方法类
    /// </summary>
    public static class DomainExtensionMethod
    {
        /// <summary>
        /// 取某个Domain的名称，根据DomainNameAttribute属性获取，如果没有设置DomainNameAttribute，则取Domain类的Name。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetDomainName(this Type type)
        {
            var attributes = (DomainNameAttribute[])type.GetCustomAttributes(typeof(DomainNameAttribute), false);
            if (attributes.Length > 0)
                return attributes.First().Value;

            return type.Name;
        }

        /// <summary>
        /// 取某个Domain的名称，根据DomainNameAttribute属性获取，如果没有设置DomainNameAttribute，则取Domain类的Name。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static string GetDomainName<T>(this DomainBase<T> domain)
        {
            return domain.GetType().GetDomainName();
        }
    }
}
