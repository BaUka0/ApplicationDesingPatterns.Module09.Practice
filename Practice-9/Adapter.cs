using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_9
{
    public interface IInternalDeliveryService
    {
        void DeliverOrder(string orderId);
        void GetDeliveryStatus(string orderId);
    }
    public class InternalDeliveryService : IInternalDeliveryService
    {
        public void DeliverOrder(string orderId)
        {
            Console.WriteLine($"Order {orderId} has been submitted for processing to the internal delivery service.");
        }
        public void GetDeliveryStatus(string orderId)
        {
            Console.WriteLine($"Checking order status {orderId}:");
            Console.WriteLine("Delivering");
        }
    }
    public class ExternalLogisticsServiceA
    {
        public void ShipItem(int itemId)
        {
            Console.WriteLine($"Item with ID {itemId} shipped by ExternalLogisticsServiceA.");
        }

        public void TrackShipment(int shipmentId)
        {
            Console.WriteLine($"Tracking the parcel with ID {shipmentId} via ExternalLogisticsServiceA:");
            Console.WriteLine("On the way");
        }
    }
    public class ExternalLogisticsServiceB
    {
        public void SendPackage(string packageInfo)
        {
            Console.WriteLine($"Package with information {packageInfo} sent by ExternalLogisticsServiceB.");
        }

        public void CheckPackageStatus(string trackingCode)
        {
            Console.WriteLine($"Checking the status of the package with the code {trackingCode} via ExternalLogisticsServiceB:");
            Console.WriteLine("Delivered"); 
        }
    }
    public class LogisticsAdapterA : IInternalDeliveryService
    {
        private readonly ExternalLogisticsServiceA _serviceA;

        public LogisticsAdapterA(ExternalLogisticsServiceA serviceA)
        {
            _serviceA = serviceA;
        }

        public void DeliverOrder(string orderId)
        {
            int itemId = int.Parse(orderId);
            _serviceA.ShipItem(itemId);
        }

        public void GetDeliveryStatus(string orderId)
        {
            int shipmentId = int.Parse(orderId);
            _serviceA.TrackShipment(shipmentId);
        }
    }
    public class LogisticsAdapterB : IInternalDeliveryService
    {
        private readonly ExternalLogisticsServiceB _serviceB;

        public LogisticsAdapterB(ExternalLogisticsServiceB serviceB)
        {
            _serviceB = serviceB;
        }

        public void DeliverOrder(string orderId)
        {
            _serviceB.SendPackage(orderId);
        }

        public void GetDeliveryStatus(string orderId)
        {
            _serviceB.CheckPackageStatus(orderId);
        }
    }
    public class DeliveryServiceFactory
    {
        public IInternalDeliveryService GetDeliveryService(string serviceType)
        {
            switch (serviceType)
            {
                case "Internal":
                    return new InternalDeliveryService();
                case "ExternalA":
                    return new LogisticsAdapterA(new ExternalLogisticsServiceA());
                case "ExternalB":
                    return new LogisticsAdapterB(new ExternalLogisticsServiceB());
                default:
                    throw new ArgumentException("Invalid delivery service type.");
            }
        }
    }
    internal class Adapter
    {
    }
}
