using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Web;
using NHibernate;
using PersistentLayer.NHibernate;

namespace WcfSalesArea.NhContext
{
    public class NhServiceBehaviorAttribute
         : Attribute, IServiceBehavior
    {

        private readonly ISessionFactory sessionFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionFactoyProvider">A property name which returns a session factory for this object.</param>
        /// <param name="provider"></param>
        public NhServiceBehaviorAttribute(string sessionFactoyProvider, Type provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", @"The object type cannot be null.");

            const BindingFlags flags = BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            PropertyInfo propertyInfo = provider.GetProperty(sessionFactoyProvider, flags);
            
            if (propertyInfo == null)
                throw new PropertyNotFoundException(provider, sessionFactoyProvider, "static");

            try
            {
                var ret = propertyInfo.GetValue(null, null);
                sessionFactory = (ISessionFactory)ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        /// <param name="endpoints"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //serviceHostBase.Description.Behaviors

            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in channelDispatcher.Endpoints)
                {
                    //var a = endpoint.DispatchRuntime.Operations.First().Formatter;

                    endpoint.DispatchRuntime.MessageInspectors
                        .Add(new NhDispatchMessageInspector(this.sessionFactory));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            
        }
    }
}