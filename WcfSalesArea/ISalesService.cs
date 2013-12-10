using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PersistentLayer.Domain;
using WcfExtensions;

namespace WcfSalesArea
{
    [ServiceContract(Namespace = "WcfSalesArea")]
    [ServiceKnownType("GetKnownTypes", typeof(WcfServiceHolder))]
    public interface ISalesService
    {
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, Method = "POST")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json), OperationContract]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json), OperationContract]
        Agency GetAgency(long id);


        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest), OperationContract]
        IEnumerable<Salesman> GetSalesman(int pageIndex, int pageSize);

        //[OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest), OperationContract]
        IEnumerable<TradeContract> GetContract(int pageIndex, int pageSize);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Agency GetFisrtAgency();
    }
}
