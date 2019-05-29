using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Lazyfitness
{
    public static class WebConfigHelper
    {
        private static Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.config");
        /// <summary>     
        /// 设置应用程序配置节点，如果已经存在此节点，则会修改该节点的值，否则添加此节点    
        /// </summary>     
        /// <param name="key">节点名称</param>     
        /// <param name="value">节点值</param>     
        public static void SetAppSetting(string key, string value)
        {
            AppSettingsSection appSetting = (AppSettingsSection)config.GetSection("appSettings");
            if (appSetting.Settings[key] == null)//如果不存在此节点，则添加     
            {
                appSetting.Settings.Add(key, value);
            }
            else//如果存在此节点，则修改     
            {
                appSetting.Settings[key].Value = value;
            }
        }

        /// <summary>
        /// 获取应用程序配置节点
        /// </summary>
        /// <param name="key">节点名</param>
        /// <returns>节点值</returns>
        public static string GetAppSetting(string key)
        {
            AppSettingsSection appSetting = (AppSettingsSection)config.GetSection("appSettings");
            if (appSetting.Settings[key] == null)
                return "";
            else
                return appSetting.Settings[key].Value;
        }

        /// <summary>     
        /// 保存所作的修改     
        /// </summary>     
        public static void Save()
        {
            config.Save();
            config = null;
        }

    }
}