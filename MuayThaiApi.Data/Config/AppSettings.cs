using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Data.Config
{
    public class AppSettings
    {
        #region Patron de Diseño
        private static AppSettings _instance;
        private static readonly object _instanceLock = new object();
        public static AppSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new AppSettings();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public string GetConectionStringFromMainProyect(string conectionString)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                       .SetBasePath(Directory.GetCurrentDirectory())
                                       .AddJsonFile("appsettings.json")
                                       .Build();
                return configuration.GetConnectionString(conectionString);
        }
    }
}
