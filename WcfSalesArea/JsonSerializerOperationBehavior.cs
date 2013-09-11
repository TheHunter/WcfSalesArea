using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Description;
using System.Web;
using Autofac;
using Autofac.Integration.Wcf;

namespace WcfSalesArea
{
    public class JsonSerializerOperationBehavior
        : DataContractSerializerOperationBehavior
    {
        //private readonly DataContractJsonSerializer serializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDescription"></param>
        public JsonSerializerOperationBehavior(OperationDescription operationDescription)
            : base(operationDescription)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="ns"></param>
        /// <param name="knownTypes"></param>
        /// <returns></returns>
        public override XmlObjectSerializer CreateSerializer(Type type, string name, string ns, IList<Type> knownTypes)
        {
            //return new DataContractJsonSerializer(type, name, WcfServiceHolder.GetKnownTypes(null));
            return new DataContractJsonSerializer(type, name, knownTypes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="ns"></param>
        /// <param name="knownTypes"></param>
        /// <returns></returns>
        public override XmlObjectSerializer CreateSerializer(Type type, System.Xml.XmlDictionaryString name, System.Xml.XmlDictionaryString ns, IList<Type> knownTypes)
        {
            //return new DataContractJsonSerializer(type, name, WcfServiceHolder.GetKnownTypes(null));
            return new DataContractJsonSerializer(type, name, knownTypes);
        }
    }
}