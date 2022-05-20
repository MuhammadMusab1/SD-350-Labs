/*
C:\Users\Muhammad Musab\Desktop\Lab1StrategyPattern\Lab1StrategyPattern\ordersList.txt

 */
Management ticketManagment = new Management();
//To populate the ordersList.txt
//ticketManagment.BookTicket(12);
//ticketManagment.BookTicket(1);
//ticketManagment.BookTicket(6);
List<Order> orders = ticketManagment.MakeOrderFromTxtFile();
Console.WriteLine(orders.Count());

public class Management
{
    public string pathOfordersList { get; set; }
    public FileInfo newOrdersListTxt { get; set; }
    public double TicketFee { get; set; }
    public CalculationContext CalculationContext { get; set; }
    public Management()
    {
        pathOfordersList = @"C:\Users\Muhammad Musab\Desktop\Lab1StrategyPattern\Lab1StrategyPattern\ordersList.txt";
        newOrdersListTxt = new FileInfo(pathOfordersList);
        TicketFee = 50;
    }
    public void BookTicket(int month)
    {
        //If the month is December, the fee will be doubled.
        //If the month is June or July, there is a 25 % discount.
        //Other months will keep the fee without a change.

        if(month == 12)
        {
            CalculationContext = new DecemberContext();
            CalculationContext.PerformCalculate(newOrdersListTxt, month, TicketFee);
        }
        else if(month == 6 || month == 7)
        {
            CalculationContext = new JuneJulyContext();
            CalculationContext.PerformCalculate(newOrdersListTxt, month, TicketFee);
        }
        else
        {
            CalculationContext = new OtherMonthContext();
            CalculationContext.PerformCalculate(newOrdersListTxt, month, TicketFee);
        }

    }
    public List<Order> MakeOrderFromTxtFile()
    {
        List<Order> orders = new List<Order>();
        using (StreamReader reader = newOrdersListTxt.OpenText()) //OPEN and READ a file's text
        {
            string fileText = reader.ReadLine();
            if(String.IsNullOrEmpty(fileText))
            {
                fileText = reader.ReadLine();
            }
            List<string> orderDetails = new List<string>();
            while(fileText != null)
            {
                orderDetails = fileText.Split(" ").ToList();
                //original fee, month, discount, final calculated fee 
                Order newOrder = new Order();
                newOrder.TicketFee = double.Parse(orderDetails[0]);
                newOrder.Month = int.Parse(orderDetails[1]);
                newOrder.Discount = double.Parse(orderDetails[2]);
                newOrder.FinalPrice = double.Parse(orderDetails[3]);
                orders.Add(newOrder);
                fileText = reader.ReadLine();
            }
        }
        return orders;
    }
}
//strategy
public interface ICalculationStrategy
{
    public void Calculate(FileInfo file, int month, double ticketFee);
    //original fee, month, discount, final calculated fee in one line:
    //50 12 0 100 //December case

}
public class DecemberStrategy : ICalculationStrategy
{
    public void Calculate(FileInfo file, int month, double ticketFee)
    {

        int discount = 0;
        double finalPrice = ticketFee * 2;

        using (StreamWriter writer = file.AppendText())
        {
            writer.WriteLine($"{ticketFee} {month} {discount} {finalPrice}");
        }
        //If the month is December, the fee will be doubled.
        //original fee, month, discount, final calculated fee in one line:
        //50 12 0 100 //December case
    }
}
public class JuneJulyStrategy : ICalculationStrategy
{
    public void Calculate(FileInfo file, int month, double ticketFee)
    {
        double discount = ticketFee * 0.25; //25%
        double finalPrice = ticketFee - discount;

        using (StreamWriter writer = file.AppendText())
        {
            writer.WriteLine($"{ticketFee} {month} {discount} {finalPrice}");
        }
        //If the month is June or July, there is a 25% discount.
        //50 6 25 37.5 (June July case)
    }
}
public class OtherMonthStartegy : ICalculationStrategy
{
    public void Calculate(FileInfo file, int month, double ticketFee)
    {
        int discount = 0;
        double finalPrice = ticketFee;

        using (StreamWriter writer = file.AppendText())
        {
            writer.WriteLine($"{ticketFee} {month} {discount} {finalPrice}");
        }
        //Other months will keep the fee without a change.
        //50 1 0 50 (Jan case)
    }
}
//Context
public abstract class CalculationContext
{
    public ICalculationStrategy CalculationStrategy { get; set; }
    public void PerformCalculate(FileInfo file, int month, double ticketFee)
    {
        CalculationStrategy.Calculate(file, month, ticketFee);
    }
}
public class DecemberContext : CalculationContext
{
    public DecemberContext()
    {
        CalculationStrategy = new DecemberStrategy();
    }
}
public class JuneJulyContext : CalculationContext
{
    public JuneJulyContext()
    {
        CalculationStrategy = new JuneJulyStrategy();
    }
}
public class OtherMonthContext : CalculationContext
{
    public OtherMonthContext()
    {
        CalculationStrategy = new OtherMonthStartegy();
    }
}

public class Order
{
    public int Month { get; set; }
    public double FinalPrice { get; set; }
    public double Discount { get; set; }
    public double TicketFee { get; set; }
}

