using Kentico.KInspector.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kentico.KInspector.Modules.Helpers
{
    public static class WebConfigHelper
    {
        public static Configuration GetWebConfig(IInstanceInfo instanceInfo)
        {
            var kenticoVersion = instanceInfo.Version;
            string pathToWebConfig = instanceInfo.Directory.ToString();

            if ((kenticoVersion >= new Version("8.0")) && !(instanceInfo.Directory.ToString().EndsWith("\\CMS\\") || instanceInfo.Directory.ToString().EndsWith("\\CMS")))
            {
                pathToWebConfig += "\\CMS";
            }

            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = pathToWebConfig + "\\web.config" };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return configuration;
        }
    }
}
