using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PersistentLayer.Domain;

namespace WcfSalesArea
{
    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(WcfServiceHolder))]
    public interface ISalesService
    {
        [OperationContract]
        IEnumerable<Salesman> GetSalesman(int pageIndex, int pageSize);

        [OperationContract]
        IEnumerable<TradeContract> GetContract(int pageIndex, int pageSize);
    }
}
