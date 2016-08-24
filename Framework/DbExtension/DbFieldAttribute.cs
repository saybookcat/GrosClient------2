using System;

namespace Framework.DbExtension
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class DbFieldAttribute : System.Attribute
    {
        /// <summary>
        /// 描述数据模型中的数据库字段，如果属性中没有DbField描述，则采用Property.Name作为数据库字段映射
        /// </summary>
        public string DbName { get; }

        public DbFieldAttribute(string dbName)
        {
            this.DbName = dbName;
        }
    }
}
