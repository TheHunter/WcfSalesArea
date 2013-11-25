using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using PersistentLayer.Domain;
using WcfJsonFormatter;
using WcfJsonFormatter.Ns;

namespace WcfSalesArea.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProxy();
            //Test();

        }

        private static void Test()
        {
            
        }

        private static void RunProxy()
        {
            //string baseAddress = "http://" + Environment.MachineName + ":8000/Service.svc";
            string baseAddress = "http://localhost:52696/SalesService.svc";

            WebHttpBinding webBinding = new WebHttpBinding
            {
                ContentTypeMapper = new RawContentMapper(),
                MaxReceivedMessageSize = 4194304,
                MaxBufferSize = 4194304,
                SendTimeout = TimeSpan.FromMinutes(4)
            };

            EndpointAddress endpoint = new EndpointAddress(baseAddress + "/json");

            using (SalesService proxy = new SalesService(webBinding, endpoint))
            {
                proxy.Endpoint.Behaviors.Add(new WebHttpJsonNetBehavior());

                try
                {
                    var cons = proxy.GetSalesman(0, 5);
                    Console.WriteLine(cons);
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
