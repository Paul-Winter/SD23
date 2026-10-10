using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;

namespace ZooStav.Wolf.Web.Infrastructure;


public class SubdomainRoutingMiddleware
{

    private static readonly HashSet<string> ReservedFirstSegments = new(StringComparer.OrdinalIgnoreCase)
    {
        "api", "css", "js", "images", "media", "lib", "fonts", "swagger", "favicon.ico",
        "staff", "account", "wolf", "health", "_framework", "_blazor", "robots.txt", "sitemap.xml"
    };

    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;
    private readonly ILogger<SubdomainRoutingMiddleware> _log;
    private readonly IServiceScopeFactory _scopeFactory;

    public SubdomainRoutingMiddleware(RequestDelegate next, IConfiguration config,
        ILogger<SubdomainRoutingMiddleware> log, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _config = config;
        _log = log;
        _scopeFactory = scopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var host = context.Request.Host.Host;
        var primaryDomain = _config["Zoo:PrimaryDomain"] ?? "zoostav.ru";
        var slug = ExtractSubdomain(host, primaryDomain);

        if (!string.IsNullOrEmpty(slug))
        {
            // Фиксируем сам факт поддомена — это нужно и для неизвестных слагов (страница-подсказка 404).
            context.Items["ZooSubdomain"] = slug;

            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var animal = await db.Animals.AsNoTracking()
                .FirstOrDefaultAsync(a => a.SubdomainSlug == slug);

            if (animal != null)
            {
                var path = context.Request.Path.Value ?? "/";
                var firstSegment = path.Split('/', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                if (firstSegment == null || !ReservedFirstSegments.Contains(firstSegment))
                {
                    var rewritten = path == "/" || string.IsNullOrEmpty(path)
                        ? $"/wolf/{animal.SubdomainSlug}"     // страница-визитка животного
                        : $"/wolf/{animal.SubdomainSlug}{path}";

                    context.Items["ZooAnimalId"] = animal.Id;
                    context.Items["ZooAnimalName"] = animal.Name;
                    context.Request.Path = new PathString(rewritten);
                }
            }
        }

        context.Response.OnStarting(() =>
        {
            if (context.Items.TryGetValue("ZooSubdomain", out var s))
                context.Response.Headers["X-Zoo-Subdomain"] = s?.ToString() ?? string.Empty;
            return Task.CompletedTask;
        });

        await _next(context);
    }

    private static string? ExtractSubdomain(string host, string primaryDomain)
    {
        if (string.IsNullOrWhiteSpace(host)) return null;

        // Основной домен и www — это не поддомен животного.
        if (host.Equals(primaryDomain, StringComparison.OrdinalIgnoreCase) ||
            host.Equals("www." + primaryDomain, StringComparison.OrdinalIgnoreCase))
            return null;

        if (host.EndsWith("." + primaryDomain, StringComparison.OrdinalIgnoreCase))
            return host[..^(primaryDomain.Length + 1)].ToLowerInvariant();

      
        if (host.EndsWith(".localhost", StringComparison.OrdinalIgnoreCase))
            return host[..^".localhost".Length].ToLowerInvariant();

        return null;
    }
}
