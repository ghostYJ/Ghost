using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ghost.Utility
{
    /// <summary>
    /// XmlLinq扩展方法
    /// </summary>
    public static class XmlLinqExtensionMethod
    {
        /// <summary>
        /// 取XElement值，若为空返回空字符串。
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetValue(this XElement e)
        {
            if (e == null)
                return string.Empty;
            return e.Value;
        }

        /// <summary>
        /// 取XAttribute值，若为空返回空字符串。
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetAttribute(this XAttribute a)
        {
            if (a == null)
                return string.Empty;
            return a.Value;
        }

        /// <summary>
        /// 取XElement子元素值，若为空返回空字符串。
        /// </summary>
        /// <param name="e"></param>
        /// <param name="elementName">子元素名称</param>
        /// <returns></returns>
        public static string GetElementValue(this XElement e, string elementName)
        {
            if (e == null)
                return string.Empty;
            XElement element = e.Element(elementName);
            if (element != null)
                return string.Empty;
            return element.Value;
        }

        /// <summary>
        /// 取XElement的Attribute属性值，若为空返回空字符串。
        /// </summary>
        /// <param name="e"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static string GetAttributeValue(this XElement e, string attributeName)
        {
            if (e == null)
                return string.Empty;
            XAttribute attr = e.Attribute(attributeName);
            if (attr == null)
                return string.Empty;
            return attr.Value;
        }
    }
}
