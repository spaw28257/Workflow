using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using Intranet.Utilities;
using EncryptDecrypt;

namespace Intranet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Se obtiene la cadena de conexión al servidor de base de datos
            Setting.ConfigurationSetting = new ConfigurationSetting()
            {
                SQLConnection = EncriptadorMD5.Decrypt(ConfigurationManager.AppSettings["SQLConnection"]),
                SQLConnectionDY = EncriptadorMD5.Decrypt(ConfigurationManager.AppSettings["SQLConnectionDYNAMICS"]),
                SQLConnectionCORP = EncriptadorMD5.Decrypt(ConfigurationManager.AppSettings["SQLConnectionCORP"]),
                Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["Timeout"])
            };
        }
    }
}
