using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TPlayerSupport;

namespace PluginHelper
{
    public static class PluginHelper
    {
        //https://blog.csdn.net/weixin_33872660/article/details/86348298
        //https://www.cnblogs.com/rinack/p/5865372.html
        static string dll_name = Assembly.GetExecutingAssembly().GetName().Name;

        #region 反射操作

        public static List<PluginDomain> GetPluginObjects(this string PluginPath, List<string> InterfaceName)
        {
            List<PluginDomain> plugins = new List<PluginDomain>();
            string[] PluginFiles = Directory.GetFiles(PluginPath);
            foreach (string file in PluginFiles)
            {
                //判断文件格式是否为.dll格式
                if (Path.GetExtension(file).ToLower() == ".dll")
                {
                    PluginDomain pluginDomain = new PluginDomain(dll_name, file, InterfaceName);
                    if (pluginDomain.isSuccess)
                    {
                        plugins.Add(pluginDomain);
                    }
                }
            }

            return plugins;
        }
        public static PluginDomain GetPluginObject(this string PluginPath, List<string> InterfaceName)
        {
            PluginDomain pluginDomain = new PluginDomain(dll_name, PluginPath, InterfaceName);
            if (pluginDomain.isSuccess)
            {
                return pluginDomain;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }


    public class PluginDomain : IDisposable
    {
        public bool isSuccess = false;
        AppDomain _appDomain = null;
        PluginAssembly _assembly = null;

        string old_dll_name, old_file = null;
        List<string> old_interfaceName = null;
        public string PluginType { get; set; }

        /// <summary>
        /// Dll域
        /// </summary>
        /// <param name="dll_name">dll名称</param>
        /// <param name="file">dll路径</param>
        /// <param name="interfaceName">抽象类名称</param>
        public PluginDomain(string dll_name, string file, List<string> interfaceName)
        {
            this.Load(dll_name, file, interfaceName);
        }

        #region 加载/卸载

        /// <summary>
        /// 重新加载dll
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            return this.Load(old_dll_name, old_file, old_interfaceName);
        }
        /// <summary>
        /// 加载新dll
        /// </summary>
        /// <param name="dll_name">dll名称</param>
        /// <param name="file">dll路径</param>
        /// <param name="interfaceName">抽象类名称</param>
        /// <returns>true加载成功 反之加载失败</returns>
        public bool Load(string dll_name, string file, List<string> interfaceName)
        {
            Unload();
            try
            {
                string name = Path.GetFileName(file);
                _appDomain = AppDomain.CreateDomain("DLLUnload_" + name);
                _assembly = (PluginAssembly)_appDomain.CreateInstanceFromAndUnwrap(dll_name + ".dll", "PluginHelper.PluginAssembly");
                _assembly.LoadAssembly(file, interfaceName);
                if (_assembly.isSuccess)
                {
                    this.PluginType = _assembly.PluginType;
                    this.old_dll_name = dll_name;
                    this.old_file = file;
                    this.old_interfaceName = interfaceName;
                    isSuccess = true;
                    return true;
                }
                else
                {
                    Unload();
                }
            }
            catch
            {
                Unload();
            }

            return false;
        }

        /// <summary>
        /// 卸载dll
        /// </summary>
        public void Unload()
        {
            if (_assembly != null)
            {
                _assembly.Unload();
                _assembly = null;
            }
            if (_appDomain != null)
            {
                isSuccess = false;
                AppDomain.Unload(_appDomain);
                _appDomain = null;
            }
        }

        /// <summary>
        /// 释放dll
        /// </summary>
        public void Dispose()
        {
            Unload();
        }

        #endregion

        #region 获取/方法

        public T GetPropertyValue<T>(string fid)
        {
            if (_assembly != null)
            {
                return _assembly.GetPropertyValue<T>(fid);
            }
            return default(T);
        }
        public object GetPropertyValue(string fid)
        {
            if (_assembly != null)
            {
                return _assembly.GetPropertyValue(fid);
            }
            return null;
        }
        public T GetMethodValue<T>(string fid, params object[] parameters)
        {
            if (_assembly != null)
            {
                return _assembly.GetMethodValue(fid, parameters).ToJson<T>();
            }
            return default(T);
        }

        #endregion

    }
    public class PluginAssembly : MarshalByRefObject
    {
        /// <summary>
        /// override the InitializeLifetimeService to return null instead of a valid ILease implementation
        /// to ensure this type of remote object never dies
        /// </summary>
        /// <returns>null</returns>
        public override object InitializeLifetimeService()
        {
            //return base.InitializeLifetimeService();
            return null; // makes the object live indefinitely
        }

        public bool isSuccess = false;
        Assembly assembly;
        public string PluginType { get; set; }
        public object obj { get; set; }
        public Type objType { get; set; }
        public void LoadAssembly(string file, List<string> interfaceNames)
        {
            assembly = Assembly.LoadFile(file);
            Type[] types = assembly.GetTypes();
            //循环所有类型
            foreach (Type t in types)
            {
                foreach (string interfaceName in interfaceNames)
                {
                    Type _Interface = t.GetInterface(interfaceName);
                    if (_Interface != null)
                    {
                        PluginType = interfaceName;
                        //创建实例
                        this.obj = assembly.CreateInstance(t.FullName);
                        this.objType = this.obj.GetType();
                        isSuccess = true;
                        return;
                    }
                }
            }
        }

        public T GetPropertyValue<T>(string fid)
        {
            PropertyInfo GetInfo = objType.GetProperty(fid);
            if (GetInfo != null)
            {
                object val = GetInfo.GetValue(obj);
                return (T)Convert.ChangeType(val, GetInfo.PropertyType);
            }
            return default(T);
        }
        public object GetPropertyValue(string fid)
        {
            PropertyInfo GetInfo = objType.GetProperty(fid);
            if (GetInfo != null)
            {
                return GetInfo.GetValue(obj);
            }
            return null;
        }
        public string GetMethodValue(string fid, params object[] parameters)
        {
            MethodInfo method = objType.GetMethod(fid);
            if (method != null)
            {
                object val = method.Invoke(obj, parameters);
                if (val != null)
                {
                    return val.ToJson();
                }
            }
            else
            {
            }
            return null;
        }

        public void Unload()
        {
            assembly = null;
            obj = null;
            objType = null;
        }
    }
}
