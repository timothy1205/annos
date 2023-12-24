
using annos_server.Models;

namespace annos_server.Services;

public class StatsService
{
    private readonly SubscriptionService subscriptionService;
    private readonly CategoryService categoryService;

    public StatsService()
    {
        subscriptionService = new SubscriptionService();
        categoryService = new CategoryService();
    }

    public async Task<Stats> GetStats()
    {
        // Gather data for calculations
        List<Category> categories = await categoryService.GetCategoriesAsync();
        List<Subscription> subscriptions = await subscriptionService.GetSubscriptionsAsync();

        int activeSubscriptions = subscriptions.Count();

        if (activeSubscriptions == 0)
        {
            return new Stats { };
        }

        // Calculate all costs per month
        Dictionary<Subscription, decimal> costsPerMonth = new Dictionary<Subscription, decimal>();
        subscriptions.ForEach(
            delegate (Subscription s)
            {
                costsPerMonth.Add(s, GetSingleCostPerMonth(s.Cycle, s.Frequency, s.Price));
            }
        );

        decimal totalCostPerMonth = costsPerMonth.Sum(e => e.Value);


        return new Stats
        {
            active_subscriptions = activeSubscriptions,
            cost_per_month = totalCostPerMonth,
            cost_per_year = totalCostPerMonth * 12,
            average_monthly_cost = totalCostPerMonth / activeSubscriptions,
            top_cost = subscriptions.Max(s => GetSingleCostPerMonth(s.Cycle, s.Frequency, s.Price)),
            amount_due = GetAmountDue(subscriptions),
            category_splits = GetCategorySplits(costsPerMonth, categories)
        };
    }

    private decimal GetSingleCostPerMonth(int cycle, int frequency, decimal price)
    {
        decimal paymentsPerMonth;
        decimal cost = -1;
        switch (cycle)
        {
            case 1:
                // Daily
                paymentsPerMonth = (decimal)30 / frequency;
                cost = price * paymentsPerMonth;
                break;
            case 2:
                // Weekly
                paymentsPerMonth = (decimal)4.35 / frequency;
                cost = price * paymentsPerMonth;
                break;
            case 3:
                // Monthly
                paymentsPerMonth = (decimal)1 / frequency;
                cost = price * paymentsPerMonth;
                break;
            case 4:
                // Yearly
                int numberOfMonths = 12 * frequency;
                cost = price / numberOfMonths;
                break;
        }

        return cost;
    }

    private decimal GetAmountDue(List<Subscription> subscriptions)
    {
        decimal amountDue = 0;
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
        DateTime endOfMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddDays(-1);

        foreach (Subscription sub in subscriptions)
        {
            int compareTomorrow = sub.NextPaymentDate.CompareTo(tomorrow);
            int compareEndOfMonth = sub.NextPaymentDate.CompareTo(endOfMonth);

            if (compareTomorrow < 0 || compareEndOfMonth > 0)
            {
                continue;
            }

            decimal timesToPay = 1;
            int daysInMonth = endOfMonth.Subtract(tomorrow).Days + 1;
            int daysRemaining = endOfMonth.Subtract(sub.NextPaymentDate).Days + 1;

            if (sub.Cycle == 1)
            {
                timesToPay = daysRemaining / sub.Frequency;
            }
            else if (sub.Cycle == 2)
            {
                decimal weeksInMonth = Math.Ceiling((decimal)daysInMonth / sub.Frequency);
                decimal weeksRemaining = Math.Ceiling((decimal)daysRemaining / 7);
                timesToPay = weeksRemaining / sub.Frequency;
            }

            amountDue += sub.Price * timesToPay;

        }

        return amountDue;
    }

    private CategorySplit[] GetCategorySplits(Dictionary<Subscription, decimal> costsPerMonth, List<Category> categories)
    {
        IDictionary<int, decimal> categoryDict = new Dictionary<int, decimal>();
        List<CategorySplit> categorySplits = new List<CategorySplit>();

        foreach (KeyValuePair<Subscription, decimal> entry in costsPerMonth)
        {
            decimal categorySum = 0;
            categoryDict.TryGetValue(entry.Key.SubscriptionCategoryId, out categorySum);

            categorySum += entry.Value;
            categoryDict[entry.Key.SubscriptionCategoryId] = categorySum;
        }

        foreach (KeyValuePair<int, decimal> entry in categoryDict)
        {
            if (entry.Value == 0)
            {
                continue;
            }

            Category category = categories.Single(c => c.Id == entry.Key);
            string categoryName = "";
            if (category.Name is not null) categoryName = category.Name;

            categorySplits.Add(new CategorySplit
            {
                name = categoryName,
                cost = entry.Value
            });
        }

        return categorySplits.ToArray();
    }
}

public class Stats
{
    public int active_subscriptions { get; set; } = 0;
    public decimal cost_per_month { get; set; } = 0;
    public decimal cost_per_year { get; set; } = 0;
    public decimal average_monthly_cost { get; set; } = 0;
    public decimal top_cost { get; set; } = 0;
    public decimal amount_due { get; set; } = 0;

    public CategorySplit[] category_splits { get; set; } = [];

}

public class CategorySplit
{
    public required string name { get; set; }
    public decimal cost { get; set; }
}