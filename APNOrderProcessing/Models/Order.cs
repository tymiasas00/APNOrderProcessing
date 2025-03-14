using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace APNOrderProcessing.Models
{
    public enum TypeOfClient
    {
        Company,
        Individual
    }
    public enum TypeOfPayment
    {
        Card,
        Cash
    }
    public enum Status
    {
        New,
        InWarehouse,
        InDelivery,
        Delivered,
        Error,
        Closed
    }
    public class Order
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal OrderPrice { get; set; }
        public TypeOfClient ClientType { get; set; }

        public string Address { get; set; } = string.Empty;
        public TypeOfPayment PaymentType { get; set; }


    }
}
