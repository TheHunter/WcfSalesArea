using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using PersistentLayer.Domain;

namespace WcfSalesArea.Client
{
    [ServiceContract]
    public interface ISalesService
    {
        //[OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest), OperationContract]
        IEnumerable<Salesman> GetSalesman(int pageIndex, int pageSize);

        //[OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest), OperationContract]
        IEnumerable<TradeContract> GetContract(int pageIndex, int pageSize);
    }

    public class SalesService
        : ClientBase<ISalesService>, ISalesService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointName"></param>
        public SalesService(string endpointName)
            : base(endpointName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="remoteAddress"></param>
        public SalesService(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        {
        }


        public IEnumerable<Salesman> GetSalesman(int pageIndex, int pageSize)
        {
            return Channel.GetSalesman(pageIndex, pageSize);
        }

        public IEnumerable<TradeContract> GetContract(int pageIndex, int pageSize)
        {
            return Channel.GetContract(pageIndex, pageSize);
        }
    }
}
