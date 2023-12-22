using Microsoft.AspNetCore.Mvc;
using annos_server.Models;
using annos_server.Services;

namespace annos_server.Controllers;

public class CategoriesController : AnnosControllerBase
{
    private readonly CategoryService categoryService;

    public CategoriesController()
    {
        categoryService = new CategoryService();
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAll()
    {
        return await categoryService.GetCategoriesAsync();
    }

    public async Task<ActionResult<Category>> Get([FromQuery(Name = "id")] int id)
    {
        Category? cat = await categoryService.GetCategoryAsync(id);
        if (cat == null)
        {
            return NotFound();
        }

        return cat;
    }

    [HttpPost]
    public async Task<ActionResult> Add(IFormCollection data)
    {
        if (MissingKeys(data, ["name"]))
        {
            return Problem("Missing data");
        }

        try
        {
            await categoryService.AddCategoryAsync(
                new Category
                {
                    Name = data["name"]
                });
            return Ok(new CategorySuccessAdd());
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
            bool updated = await categoryService.UpdateCategoryAsync(
                id,
                new Category
                {
                    Name = data["name"]
                });

            if (updated) return Ok(new CategorySuccessEdit());
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

        bool deleted = await categoryService.DeleteCategoryAsync(id);
        if (deleted) return Ok(new CategorySuccessDelete());
        else return Problem("Invalid id");

    }

}

public class CategorySuccess
{
    public string status { get; } = "Success";
}

public class CategorySuccessAdd : CategorySuccess
{
    public string message { get; } = "Category added successfully";
}

public class CategorySuccessEdit : CategorySuccess
{
    public string message { get; } = "Category updated successfully";
}

public class CategorySuccessDelete : CategorySuccess
{
    public string message { get; } = "Category deleted successfully";
}