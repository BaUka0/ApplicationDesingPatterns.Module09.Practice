using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_9
{
    public class Program
    {
        static void Main(string[] args)
        {
            IReport salesReport = new SalesReport();

            salesReport = new DateFilterDecorator(salesReport, new DateTime(2023, 1, 1), new DateTime(2023, 12, 31));
            salesReport = new SortingDecorator(salesReport, "SalesAmount");
            salesReport = new CsvExportDecorator(salesReport);

            string reportData = salesReport.Generate();
            Console.WriteLine(reportData);


            // Используем внутреннюю службу доставки
            DeliveryServiceFactory factory = new DeliveryServiceFactory();
            IInternalDeliveryService internalDeliveryService = factory.GetDeliveryService("Internal");
            internalDeliveryService.DeliverOrder("12345");
            internalDeliveryService.GetDeliveryStatus("12345");

            // Используем стороннюю службу доставки
            IInternalDeliveryService externalDeliveryService = factory.GetDeliveryService("ExternalA");
            externalDeliveryService.DeliverOrder("67890");
            externalDeliveryService.GetDeliveryStatus("67890");

            // Используем другую стороннюю службу доставки
            externalDeliveryService = factory.GetDeliveryService("ExternalB");
            externalDeliveryService.DeliverOrder("101112");
            externalDeliveryService.GetDeliveryStatus("101112");
        }
    }
}
