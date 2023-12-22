using annos_server.Models;
using Microsoft.EntityFrameworkCore;

namespace annos_server.Services;

public class CategoryService
{
    private readonly PostgresContext ctx;

    public CategoryService()
    {
        ctx = new PostgresContext();
        ctx.Database.EnsureCreated();
    }

    public async Task AddCategoryAsync(Category cat)
    {
        ctx.Categories.Add(cat);
        await ctx.SaveChangesAsync();
    }

    /*
        Take in id as a separate parameter and set it here to
        better imply that updatedCategory is a "dummy" object
        until now.
    */
    public async Task<bool> UpdateCategoryAsync(int id, Category updatedCategory)
    {
        updatedCategory.Id = id;

        try
        {
            ctx.Categories.Update(updatedCategory);
            await ctx.SaveChangesAsync();
            return true;

        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        try
        {
            ctx.Categories.Remove(new Category { Id = id });
            await ctx.SaveChangesAsync();
            return true;

        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public Task<List<Category>> GetCategoriesAsync()
    {
        return ctx.Categories.ToListAsync();
    }

    public ValueTask<Category?> GetCategoryAsync(int id)
    {
        return ctx.Categories.FindAsync(id);
    }
}

