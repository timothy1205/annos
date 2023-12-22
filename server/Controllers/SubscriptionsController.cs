using Microsoft.AspNetCore.Mvc;
using annos_server.Models;
using annos_server.Services;

namespace annos_server.Controllers;

public class SubscriptionsController : AnnosControllerBase
{
    private readonly SubscriptionService subscriptionService;
    private readonly CategoryService categoryService;

    public SubscriptionsController()
    {
        subscriptionService = new SubscriptionService();
        categoryService = new CategoryService();
    }

    [HttpGet]
    public async Task<ActionResult<List<Subscription>>> GetAll()
    {
        return await subscriptionService.GetSubscriptionsAsync();
    }

    public async Task<ActionResult<Subscription>> Get([FromQuery(Name = "id")] int id)
    {
        Subscription? sub = await subscriptionService.GetSubscriptionAsync(id);
        if (sub == null)
        {
            return NotFound();
        }

        return sub;
    }

    [HttpPost]
    public async Task<ActionResult> Add(IFormCollection data)
    {
        if (MissingKeys(data, ["name", "price", "frequency", "cycle", "category_id", "next_payment"]))
        {
            return Problem("Missing data");
        }

        try
        {

            int categoryId = int.Parse(data["category_id"]!.ToString());
            Category? cat = await categoryService.GetCategoryAsync(categoryId);

            if (cat is null)
            {
                return Problem("Invalid category");
            }

            int frequency = int.Parse(data["frequency"]!.ToString());
            if (frequency < 1 || frequency > 4)
            {
                return Problem("Frequency range is 1-4");
            }

            await subscriptionService.AddSubscriptionAsync(
                new Subscription
                {
                    Name = data["name"],
                    Price = double.Parse(data["price"]!.ToString()),
                    Cycle = int.Parse(data["cycle"]!.ToString()),
                    Frequency = frequency,
                    NextPaymentDate = DateTime.Parse(data["next_payment"]!.ToString()),
                    SubscriptionCategoryId = categoryId
                });
            return Ok(new SubscriptionSuccessAdd());
        }
        catch (FormatException)
        {
            return Problem("Invalid data");
        }
    }


    public async Task<ActionResult> Edit(IFormCollection data)
    {
        if (MissingKeys(data, ["name", "price", "frequency", "cycle", "category_id", "next_payment"]))
        {
            return Problem("Missing data");
        }

        try
        {
            int id = int.Parse(data["id"]!.ToString());

            int categoryId = int.Parse(data["category_id"]!.ToString());
            Category? cat = await categoryService.GetCategoryAsync(categoryId);

            if (cat is null)
            {
                return Problem("Invalid category");
            }

            int frequency = int.Parse(data["frequency"]!.ToString());
            if (frequency < 1 || frequency > 4)
            {
                return Problem("Frequency range is 1-4");
            }

            bool updated = await subscriptionService.UpdateSubscriptionAsync(
                id,
                new Subscription
                {
                    Name = data["name"],
                    Price = double.Parse(data["price"]!.ToString()),
                    Frequency = frequency,
                    Cycle = int.Parse(data["cycle"]!.ToString()),
                    NextPaymentDate = DateTime.Parse(data["next_payment"]!.ToString()),
                    SubscriptionCategoryId = categoryId
                });

            if (updated) return Ok(new SubscriptionSuccessEdit());
            else return Problem("Invalid id");
        }
        catch (FormatException)
        {
            return Problem("Invalid data");
        }
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery(Name = "id")] int id)
    {
        if (id == 0)
        {
            return Problem("Invalid id");
        }

        bool deleted = await subscriptionService.DeleteSubscriptionAsync(id);
        if (deleted) return Ok(new SubscriptionSuccessDelete());
        else return Problem("Invalid id");

    }

}

public class SubscriptionSuccess
{
    public string status { get; } = "Success";
}

public class SubscriptionSuccessAdd : SubscriptionSuccess
{
    public string message { get; } = "Subscription added successfully";
}

public class SubscriptionSuccessEdit : SubscriptionSuccess
{
    public string message { get; } = "Subscription updated successfully";
}

public class SubscriptionSuccessDelete : SubscriptionSuccess
{
    public string message { get; } = "Subscription deleted successfully";
}