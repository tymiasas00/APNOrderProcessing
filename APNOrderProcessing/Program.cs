using APNOrderProcessing.Models;

class Program
{
    static readonly List<Order> orders = [];

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Order Processing System");
            Console.WriteLine("1. Create a sample order");
            Console.WriteLine("2. Send order to warehouse");
            Console.WriteLine("3. Ship order");
            Console.WriteLine("4. View orders");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1": CreateOrder(); break;
                case "2": ProcessWarehouse(); break;
                case "3": ProcessShipping(); break;
                case "4": ViewOrders(); break;
                case "5": return;
                default: Console.WriteLine("Invalid choice. Press any key to continue..."); Console.ReadKey(); break;
            }
        }
    }

    static void CreateOrder()
    {
        try
        {
            string productName;
            do
            {
                Console.Write("Product Name: ");
                productName = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(productName))
                {
                    Console.WriteLine("Product name cannot be empty. Please enter a valid product name.");
                }
            } while (string.IsNullOrWhiteSpace(productName));

            decimal orderPrice;
            do
            {
                Console.Write("Order Price: ");
                if (!decimal.TryParse(Console.ReadLine(), out orderPrice) || orderPrice <= 0)
                {
                    Console.WriteLine("Invalid order price.");
                }
            } while (orderPrice <= 0);

            TypeOfClient clientType;
            do
            {
                Console.Write("Client Type (1: Company, 2: Individual): ");
                string? clientTypeInput = Console.ReadLine();
                if (clientTypeInput == "1")
                {
                    clientType = TypeOfClient.Company;
                    break;
                }
                else if (clientTypeInput == "2")
                {
                    clientType = TypeOfClient.Individual;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid client type. Please enter 1 for Company or 2 for Individual.");
                }
            } while (true);

            string address;
            do
            {
                Console.Write("Delivery Address: ");
                address = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(address))
                {
                    Console.WriteLine("Address cannot be empty. Please enter a valid address.");
                }
            } while (string.IsNullOrWhiteSpace(address));

            Console.Write("Payment Type (1: Card, 2: Cash): ");
            TypeOfPayment paymentType = (Console.ReadLine() == "1") ? TypeOfPayment.Card : TypeOfPayment.Cash;

            Order newOrder = new(productName, orderPrice, clientType, address, paymentType);
            orders.Add(newOrder);
            Console.WriteLine("Order created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void ProcessWarehouse()
    {
        var order = SelectOrder(Status.New);
        if (order == null) return;

        if (order.PaymentType == TypeOfPayment.Cash && order.OrderPrice >= 2500)
        {
            order.OrderStatus = Status.ReturnedToClient;
            Console.WriteLine("Order returned to client due to high cash payment.");
        }
        else
        {
            order.OrderStatus = Status.InWarehouse;
            Console.WriteLine("Order moved to warehouse.");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void ProcessShipping()
    {
        var order = SelectOrder(Status.InWarehouse);
        if (order == null) return;

        if (string.IsNullOrWhiteSpace(order.Address))
        {
            order.OrderStatus = Status.Error;
            Console.WriteLine("Error: Order has no shipping address.");
        }
        else
        {
            order.OrderStatus = Status.InDelivery;
            Console.WriteLine("Order is now in delivery. It will now be shipped.");
            Thread.Sleep(1000);
            order.OrderStatus = Status.Closed;
            Console.WriteLine("Order has been shipped and closed.");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void ViewOrders()
    {
        if (orders.Count == 0)
        {
            Console.WriteLine("No orders available.");
        }
        else
        {
            foreach (var order in orders)
            {
                Console.WriteLine(order.ToString());
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static Order? SelectOrder(Status status)
    {
        var availableOrders = orders.Where(o => o.OrderStatus == status).ToList();
        if (availableOrders.Count == 0)
        {
            Console.WriteLine("No available orders with the required status.");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine("Select an order:");
        for (int i = 0; i < availableOrders.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availableOrders[i].ProductName} - {availableOrders[i].OrderPrice} ({availableOrders[i].OrderStatus})");
        }
        Console.Write("Enter order number: ");
        if (int.TryParse(Console.ReadLine(), out int orderIndex) && orderIndex > 0 && orderIndex <= availableOrders.Count)
        {
            return availableOrders[orderIndex - 1];
        }
        Console.WriteLine("Invalid selection.");
        Console.ReadKey();
        return null;
    }
}
