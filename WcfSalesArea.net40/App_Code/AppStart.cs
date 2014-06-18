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
using EntityModel;
using NHibernate;
using PersistentLayer;
using PersistentLayer.Domain;
using PersistentLayer.NHibernate;
using PersistentLayer.NHibernate.Impl;
using WcfExtensions.Configuration;

namespace WcfSalesArea
{
    /// <summary>
    /// 
    /// </summary>
    public class AppStart
    {

        public static void AppInitialize()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<SalesService>();

            builder.Register(context => WcfServiceHolder.DefaultSessionFactory)
                .As<ISessionFactory>()
                .SingleInstance();

            builder.Register<Func<ISession>>(context => context.Resolve<ISessionFactory>().OpenSession)
                   .AsSelf();

            builder.Register(context => new SessionContextProvider(context.Resolve<Func<ISession>>()))
                   .As<ISessionContextProvider>();

            builder.Register(context => new EnterpriseRootDAO<IEntity>(context.Resolve<ISessionContextProvider>()))
                   .As<INhRootPagedDAO<IEntity>>();

            builder.RegisterType<SalesService>()
                .As<ISalesService>();

            AutofacHostFactory.Container = builder.Build();
        }

    }
}