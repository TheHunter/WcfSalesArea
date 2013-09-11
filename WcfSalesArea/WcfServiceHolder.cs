using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using NHibernate;
using PersistentLayer.Domain;
using PersistentLayer.NHibernate;

namespace WcfSalesArea
{
    /// <summary>
    /// 
    /// </summary>
    public static class WcfServiceHolder
    {
        private static readonly ISessionFactory Sessionfactory;
        private static readonly IEnumerable<Type> KnownTypes;

        static WcfServiceHolder()
        {
            string rootPath = HostingEnvironment.MapPath("~");

            XmlTextReader configReader = new XmlTextReader(File.OpenRead(string.Format("{0}/Cfg/Configuration.xml", rootPath)));
            DirectoryInfo dir = new DirectoryInfo(string.Format("{0}/MappingsXml", rootPath));
            NhConfigurationBuilder bld = new NhConfigurationBuilder(configReader, dir);

            bld.SetProperty("connection.connection_string", GetConnectionString(rootPath));
            bld.BuildSessionFactory();

            Sessionfactory = bld.SessionFactory;

            KnownTypes = Assembly.GetAssembly(typeof(Salesman)).GetTypes();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        private static string GetConnectionString(string rootPath)
        {
            string output = rootPath + "db\\";

            var str = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
            return string.Format(str, output);
        }

        /// <summary>
        /// 
        /// </summary>
        public static ISessionFactory DefaultSessionFactory
        {
            get
            {
                return Sessionfactory;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            return KnownTypes;
        }

    }
}