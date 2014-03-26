using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Autofac.Integration.Wcf;
using NHibernate.Criterion;
using NHibernate.Impl;
using PersistentLayer;
using PersistentLayer.Domain;
using PersistentLayer.NHibernate; 
//using PersistentLayer.NHibernate.WCF;
using Autofac;
using WcfExtensions;

namespace WcfSalesArea
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    [AspNetCompatibilityRequirements(RequirementsMode =
        AspNetCompatibilityRequirementsMode.Allowed)]
    public class SalesService : ISalesService
    {

        private readonly INhPagedDAO customPagedDAO;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customPagedDAO"></param>
        public SalesService(INhPagedDAO customPagedDAO)
        {
            this.customPagedDAO = customPagedDAO;
        }

        
        public Agency GetAgency(long id)
        {
            return this.customPagedDAO.FindBy<Agency, long>(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Salesman> GetSalesman(int pageIndex, int pageSize)
        {
            var result = this.customPagedDAO
                             .GetIndexPagedResult<Salesman>(pageIndex, pageSize, DetachedCriteria.For<Salesman>());

            return result.GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<TradeContract> GetContract(int pageIndex, int pageSize)
        {
            var result = this.customPagedDAO
                             .GetIndexPagedResult<TradeContract>(pageIndex, pageSize,
                                                                 DetachedCriteria.For<TradeContract>());

            return result.GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Agency GetFisrtAgency()
        {
            return this.customPagedDAO.FindBy<Agency, long>(1);
        }
    }
}
