using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Web;

namespace WcfSalesArea
{
    public class JsonDataContractFormatAttribute
        : Attribute, IOperationBehavior
    {
        private IOperationBehavior innerFormatterBehavior;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //new code
            if (innerFormatterBehavior != null)
            {
                innerFormatterBehavior.AddBindingParameters(operationDescription, bindingParameters);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="clientOperation"></param>
        public void ApplyClientBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
            ReplaceSerializerOperationBehavior(operationDescription);

            if (innerFormatterBehavior != null && clientOperation.Formatter == null)
            {
                // no formatter has been applied yet
                innerFormatterBehavior.ApplyClientBehavior(operationDescription, clientOperation);
            }
            //runtime.Formatter = new QueryStringFormatter(description.Name, runtime.SyncMethod.GetParameters(), runtime.Formatter, runtime.Action, endpointAddress);
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            ReplaceSerializerOperationBehavior(operationDescription);

            if (innerFormatterBehavior != null && dispatchOperation.Formatter == null)
            {
                // no formatter has been applied yet
                innerFormatterBehavior.ApplyDispatchBehavior(operationDescription, dispatchOperation);
            }
            //runtime.Formatter = new QueryStringFormatter(description.Name, description.SyncMethod.GetParameters(), runtime.Formatter);
        }

        public void Validate(OperationDescription operationDescription)
        {
            if (innerFormatterBehavior != null)
            {
                innerFormatterBehavior.Validate(operationDescription);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        private void ReplaceSerializerOperationBehavior(OperationDescription description)
        {
            DataContractSerializerOperationBehavior dcs = description.Behaviors.Find<DataContractSerializerOperationBehavior>();

            if (dcs != null)
                description.Behaviors.Remove(dcs);

            this.innerFormatterBehavior = dcs;
            description.Behaviors.Add(new JsonSerializerOperationBehavior(description));
            //description.Messages.First().Body
        }
    }
}