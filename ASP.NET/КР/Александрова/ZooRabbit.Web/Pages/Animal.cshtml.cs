using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooRabbit.Web.Data;
using ZooRabbit.Web.Models;

namespace ZooRabbit.Web.Pages;

public class AnimalModel : PageModel
{
    private readonly ZooDbContext _db;
    public AnimalModel(ZooDbContext db) => _db = db;

    public Animal? Animal { get; private set; }

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        Animal = await _db.Animals.AsNoTracking().FirstOrDefaultAsync(a => a.Slug == slug);
        if (Animal is null) Response.StatusCode = 404;
        return Page();
    }
}
