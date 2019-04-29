using System;
using System.Collections;
using System.Collections.Generic;

namespace Ghost.Utility
{
    //1、IEnumerator：提供在普通集合中遍历的接口，有Current，MoveNext()，Reset()，其中Current返回的是object类型。
    // 2、IEnumerable： 暴露一个IEnumerator，支持在普通集合中的遍历。
    //3、IEnumerator<T>：继承自IEnumerator，有Current属性，返回的是T类型。
    //4、IEnumerable<T>：继承自IEnumerable，暴露一个IEnumerator<T>，支持在泛型集合中遍历。

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumHelper<T> where T : struct
    {
        public static IEnumerable<T> AsEnumerable()
        {
            //获取泛型数据类型
            Type enumType = typeof(T);
            //判断是否为枚举类型
            if (!enumType.IsEnum)
                throw new NotSupportedException(string.Format("{0}必须为枚举类型。", enumType));
            //实例化枚举器
            EnumQuery<T> query = new EnumQuery<T>();
            //返回枚举器，该枚举器可进行简单迭代(利用list枚举数)
            return query;
        }
    }

    //实现了IEnmerable<T>接口的集合，是强类型的。它为子对象的迭代提供类型更加安全的方式。
    //只要继承IEnmerable<T>泛型接口就必须实现GetEnumerator()方法，该方法返回可迭代的枚举器，也就是说EnumQuery<T>类可将泛型T转成枚举数，而枚举数是指循环访问其关联集合的对象。

    /// <summary>
    /// 实现返回一个循环访问集合的枚举器
    /// </summary>
    /// <typeparam name="T">泛型枚举类型</typeparam>
    class EnumQuery<T> : IEnumerable<T>
    {
        private List<T> _list;

        public IEnumerator<T> GetEnumerator()
        {
            //所有的数组都继承自Array，而Array实现了IEnumerable接口
            //获取T中常数值数组
            Array items = Enum.GetValues(typeof(T));
            //定义List集合长度，其中list集合内的数据类型为T
            _list = new List<T>(items.Length);
            foreach (var item in items)
                _list.Add((T)item);
            //List<T>继承IEnumerable<T>泛型接口，也实现了返回枚举数的GetEnumerator()方法
            //返回list集合的枚举数
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
