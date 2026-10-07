using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace StavZoo.Pages;

public class IndexModel(ZooDb db) : PageModel
{
    public List<Animal> Animals { get; private set; } = new();
    public async Task OnGetAsync() => Animals = await db.Animals.OrderBy(a => a.Name).ToListAsync();
}
