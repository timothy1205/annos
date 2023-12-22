using System.Text.Json;
using annos_server.Models;
using Microsoft.EntityFrameworkCore;

namespace annos_server.Services;

public class SubscriptionService
{
    private readonly PostgresContext ctx;

    public SubscriptionService()
    {
        ctx = new PostgresContext();
        ctx.Database.EnsureCreated();
    }

    public async Task AddSubscriptionAsync(Subscription sub)
    {
        Console.WriteLine(JsonSerializer.Serialize(sub));
        Console.Out.Flush();

        ctx.Subscriptions.Add(sub);
        await ctx.SaveChangesAsync();
    }

    /*
        Take in id as a separate parameter and set it here to
        better imply that updatedSubscription is a "dummy" object
        until now.
    */
    public async Task<bool> UpdateSubscriptionAsync(int id, Subscription updatedSubscription)
    {
        updatedSubscription.Id = id;

        try
        {
            ctx.Subscriptions.Update(updatedSubscription);
            await ctx.SaveChangesAsync();
            return true;

        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteSubscriptionAsync(int id)
    {
        try
        {
            ctx.Subscriptions.Remove(new Subscription { Id = id });
            await ctx.SaveChangesAsync();
            return true;

        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public Task<List<Subscription>> GetSubscriptionsAsync()
    {
        return ctx.Subscriptions.ToListAsync();
    }

    public ValueTask<Subscription?> GetSubscriptionAsync(int id)
    {
        return ctx.Subscriptions.FindAsync(id);
    }
}

