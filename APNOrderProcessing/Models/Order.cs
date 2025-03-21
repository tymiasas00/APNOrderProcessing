﻿namespace APNOrderProcessing.Models
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
        ReturnedToClient,
        Error,
        Closed
    }
    public class Order
    {
        public int Id { get; private set; }
        public string ProductName { get; private set; }
        public decimal OrderPrice { get; private set; }
        public TypeOfClient ClientType { get; private set; }

        public string? Address { get; private set; } = string.Empty;
        public TypeOfPayment PaymentType { get; private set; }

        public Status OrderStatus { get; private set; }

        public Order(
            string productName,
            decimal orderPrice,
            TypeOfClient clientType,
            string address,
            TypeOfPayment paymentType,
            Status orderStatus = Status.New)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentNullException(
                    nameof(productName),
                    "Product name cannot be empty.");

            if (orderPrice <= 0)
                throw new ArgumentException(
                    "Incorrect order price.",
                    nameof(orderPrice));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(
                    nameof(address),
                    "Address cannot be empty.");

            ProductName = productName;
            OrderPrice = orderPrice;
            ClientType = clientType;
            Address = address;
            PaymentType = paymentType;
            OrderStatus = orderStatus;

            if (PaymentType == TypeOfPayment.Cash && orderPrice >= 2500)
                OrderStatus = Status.ReturnedToClient;
        }
        public override string ToString()
        {
            return $"Product name: {ProductName}, Order price: {OrderPrice}, Client Type: {ClientType}, Address: {Address}, Payment type: {PaymentType}, Order status: {OrderStatus}";
        }
        public void UpdateOrderStatus(Status newStatus)
        {
            OrderStatus = newStatus;
        }

    } }
