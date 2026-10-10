using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;

namespace ZooStav.Wolf.Web.Pages.Staff;

/// <summary>
/// Журнал изменений: кто, когда и что менял. Фильтры — период, специалист, действие, сущность.
/// </summary>
public class AuditModel : PageModel
{
    private readonly AppDbContext _db;

    public AuditModel(AppDbContext db) => _db = db;

    public AuditPageViewModel Vm { get; private set; } = new();

    public async Task OnGetAsync(DateTime? from, DateTime? to, string? userId, string? action,
        string? entity, int page = 1)
    {
        var q = _db.AuditLogs.AsNoTracking().AsQueryable();

        if (from.HasValue) q = q.Where(a => a.TimestampUtc >= from.Value.Date.ToUniversalTime());
        if (to.HasValue) q = q.Where(a => a.TimestampUtc < to.Value.Date.AddDays(1).ToUniversalTime());
        if (!string.IsNullOrEmpty(userId)) q = q.Where(a => a.UserId == userId);
        if (!string.IsNullOrEmpty(action)) q = q.Where(a => a.Action == action);
        if (!string.IsNullOrEmpty(entity)) q = q.Where(a => a.EntityName == entity);

        const int pageSize = 25;
        if (page < 1) page = 1;

        var total = await q.CountAsync();
        Vm = new AuditPageViewModel
        {
            Page = new PagedResult<Models.AuditLog>
            {
                Items = await q.OrderByDescending(a => a.TimestampUtc).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            },
            From = from,
            To = to,
            UserId = userId,
            Action = action,
            Entity = entity,
            Users = await _db.Users.AsNoTracking().OrderBy(u => u.DisplayName).ToListAsync()
        };

        ViewData["Title"] = "Журнал изменений";
    }
}
