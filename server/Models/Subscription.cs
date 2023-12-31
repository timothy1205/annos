using System.Text.Json.Serialization;

namespace annos_server.Models;

public class Subscription
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }

    public int Frequency { get; set; }
    public int Cycle { get; set; }

    public int SubscriptionCategoryId { get; set; }

    [JsonIgnore]
    public Category? SubscriptionCategory { get; set; }
    private DateTime _NextPaymentDate;
    public DateTime NextPaymentDate
    {
        get { return _NextPaymentDate; }
        set
        {
            _NextPaymentDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}
