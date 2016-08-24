using System;
using System.Collections.Generic;
using System.IO;
using Framework.FileHelp;

namespace Framework.Configuration
{
    /// <summary>
    /// 初始化时调用ConfigManager.Instance.Init(workPath)
    /// </summary>
    public class ConfigManager
    {
        private const string Ext = ".json";
        private readonly Dictionary<Type, object> _register;

        private ConfigManager()
        {
            _register = new Dictionary<Type, object>();
        }

        /// <summary>
        /// ConfigManger初始化时调用
        /// </summary>
        /// <param name="workPath"></param>
        public void Init(string workPath)
        {
            _workPath = workPath;
            Load();
        }

        private static string _workPath;

        private void Load()
        {
            if (FileHelper.IsFileOrDirectoryExists(_workPath))
            {
                FileHelper.CreateDirectoryByFilePath(_workPath);
            }
        }

        private static volatile ConfigManager _configManager;
        private static readonly object SyncRoot = new object();

        public static ConfigManager Instance
        {
            get
            {
                if (_configManager == null)
                {
                    lock (SyncRoot)
                    {
                        if (_configManager == null)
                        {
                            _configManager = new ConfigManager();
                        }
                    }
                }
                return _configManager;
            }
        }

        public T Get<T>() where T : class, new()
        {
            Type type = typeof(T);
            var typeName = type.ToString();
            string fullName = Path.Combine(_workPath, typeName + Ext);
            if (_register.ContainsKey(type))
            {
                return _register[type] as T;
            }

            if (!File.Exists(fullName))
            {
                return new T();
            }

            JsonSerializationHelper<T> jsonSerializationHelper = new JsonSerializationHelper<T>();
            T result = jsonSerializationHelper.DeserializeForPath(fullName);
            _register.Add(type, result);
            return result;
        }

        public void Set<T>(T t)
        {
            if (t == null) return;
            var type = typeof(T);
            if (_register.ContainsKey(type))
            {
                _register[type] = t;
            }
            else
            {
                _register.Add(type, t);
            }
            string typeName = type.ToString();
            string fullName = Path.Combine(_workPath, typeName + Ext);
            var streamSerializationHelper = new JsonSerializationHelper<T>();
            streamSerializationHelper.SerializeForPath(fullName, t);
        }
    }
}
