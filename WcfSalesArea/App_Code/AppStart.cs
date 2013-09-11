using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Web.Hosting;
using System.Xml;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Wcf;
using NHibernate;
using PersistentLayer;
using PersistentLayer.Domain;
using PersistentLayer.NHibernate;
using PersistentLayer.NHibernate.Impl;

namespace WcfSalesArea
{
    /// <summary>
    /// 
    /// </summary>
    public class AppStart
    {
        /// <summary>
        /// 
        /// </summary>
        public static void AppInitialize()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<SalesService>();

            builder.Register(n => new EnterprisePagedDAO(new SessionManager(WcfServiceHolder.DefaultSessionFactory)))
                .As<INhPagedDAO>()
                .SingleInstance();

            builder.Register(n => new SalesService(n.Resolve<INhPagedDAO>()))
                .As<ISalesService>();

            AutofacHostFactory.Container = builder.Build(ContainerBuildOptions.Default);
        }

    }
}