namespace annos_server.Models;

public class Subscription
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public DateTime NextPaymentDate { get; set; }
}