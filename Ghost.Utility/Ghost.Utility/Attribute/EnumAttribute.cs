using System;

namespace Ghost.Utility
{
    //特性是一个对象，它可以加载到程序集及程序集的对象中，这些对象包括 程序集本身、模块、类、接口、结构、构造函数、方法、方法参数等，加载了特性的对象称作特性的目标。

    //特性是为程序添加元数据(描述数据的数据)的一种机制，通过它可以给编译器提供指示或者提供对数据的说明

    //根据约定，所有特性名称都以单词“Attribute”结束，以便将它们与“.NET Framework”中的其他项区分。但是，在代码中使用特性时，不需要指定 attribute 后缀。例如，[DllImport] 虽等效于 [DllImportAttribute]，但 DllImportAttribute 才是该特性在 .NET Framework 中的实际名称

    /*AttributeUsage*/
    //Assembly			可以对程序集应用特性。
    //Module			可以对模块应用特性。
    //Module			指的是可移植的可执行文件（.dll 或.exe），而非 Visual Basic 标准模块。
    //Class				可以对类应用特性。
    //Struct			可以对结构应用特性，即值类型。
    //Enum				可以对枚举应用特性。
    //Constructor		可以对构造函数应用特性。
    //Method			可以对方法应用特性。
    //Property			可以对属性应用特性。
    //Field				可以对字段应用特性。
    //Event				可以对事件应用特性。
    //Interface			可以对接口应用特性。
    //Parameter			可以对参数应用特性。
    //Delegate			可以对委托应用特性。
    //ReturnValue		可以对返回值应用特性。
    //GenericParameter	可以对泛型参数应用特性。
    //All				可以对任何应用程序元素应用特性。

    /// <summary>
    /// 枚举String特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]//继承自定义属性基类--------如何定义固定类型的特性（如该特性只允许Enum添加）？？
    public class EnumStringAttribute : Attribute
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        public EnumStringAttribute(string value) //采用设置构造函数,也可使用默认无参构造函数
        {
            Value = value;
        }
    }

    /// <summary>
    /// 枚举Text特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class EnumTextAttribute : Attribute
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        public EnumTextAttribute(string value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// 枚举Disabled特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class EnumDisabledAttribute : Attribute
    {
        /// <summary>
        /// 值
        /// </summary>
        public bool Value { get; protected set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        public EnumDisabledAttribute(bool value)
        {
            Value = value;
        }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class EnumIndexAttribute : Attribute
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get;protected set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        public EnumIndexAttribute(string value)
        {
            Value = value;
        }
    }
}
