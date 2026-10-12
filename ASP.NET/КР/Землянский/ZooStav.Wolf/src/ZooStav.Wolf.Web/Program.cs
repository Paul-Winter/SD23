using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ZooStav.Wolf.Web.Api;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// ---------- База данных ----------

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=zoostav.wolf.db"));

// ---------- Аутентификация: Cookie (веб-интерфейс) + JWT (API) ----------
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtOptions = new JwtOptions();
jwtSection.Bind(jwtOptions);

builder.Services.AddAuthentication(options =>
{
   
    options.DefaultScheme = "ZooAuth";
    options.DefaultChallengeScheme = CookieAuthService.Scheme;
    options.DefaultSignInScheme = CookieAuthService.Scheme;
})
.AddPolicyScheme("ZooAuth", "Cookie работника или JWT", options =>
{
    options.ForwardDefaultSelector = ctx =>
        ctx.Request.Headers.Authorization.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? JwtBearerDefaults.AuthenticationScheme
            : CookieAuthService.Scheme;
})
.AddCookie(CookieAuthService.Scheme, options =>
{
    options.Cookie.Name = CookieAuthService.CookieName;
    options.LoginPath = "/staff/login";
    options.LogoutPath = "/staff/logout";
    options.AccessDeniedPath = "/staff/denied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});

builder.Services.AddAuthorization(options =>
{
    // --- Политики для страниц Razor (cookie-аутентификация, при отказе — редирект на /staff/login) ---
    options.AddPolicy("EmployeeOnly", p => p.RequireRole(Roles.Employees));
    options.AddPolicy("DiaryEditor", p => p.RequireRole(Roles.DiaryEditors));
    options.AddPolicy("AdminOnly", p => p.RequireRole(Roles.Admin));
    options.AddPolicy("AuditReader", p => p.RequireRole(Roles.Admin, Roles.Veterinarian));

    // --- Политики для API: явно требуют JWT, чтобы вместо редиректа возвращался 401 Unauthorized ---
    options.AddPolicy("ApiAuth", p =>
    {
        p.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        p.RequireAuthenticatedUser();
    });
    options.AddPolicy("ApiEmployee", p =>
    {
        p.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        p.RequireRole(Roles.Employees);
    });
    options.AddPolicy("ApiDiaryEditor", p =>
    {
        p.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        p.RequireRole(Roles.DiaryEditors);
    });
    options.AddPolicy("ApiAdminOnly", p =>
    {
        p.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        p.RequireRole(Roles.Admin);
    });
    options.AddPolicy("ApiAuditReader", p =>
    {
        p.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        p.RequireRole(Roles.MedicalStaff);
    });
});


builder.Services.ConfigureHttpJsonOptions(o =>
{
    o.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    o.SerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});

builder.Services.AddSingleton<System.Text.Encodings.Web.HtmlEncoder>(
    System.Text.Encodings.Web.HtmlEncoder.Create(System.Text.Unicode.UnicodeRanges.All));

// ---------- Веб-камера вольера ----------
builder.Services.Configure<WebcamOptions>(builder.Configuration.GetSection("Webcam"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<WebcamOptions>>().Value);
builder.Services.AddHttpClient();
builder.Services.AddSingleton<WebcamService>();
builder.Services.AddHostedService<WebcamStreamService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CurrentUser>();
builder.Services.AddScoped<CookieAuthService>();
builder.Services.AddScoped<AuditService>();builder.Services.AddScoped<DiaryService>();
builder.Services.AddScoped<DonationService>();
builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddSingleton<PasswordService>();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Staff", "EmployeeOnly");
    options.Conventions.AllowAnonymousToPage("/Staff/Login");
    options.Conventions.AllowAnonymousToPage("/Staff/Denied");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ZooStav — API страницы северного волка",
        Version = "v1",
        Description = "Публичный и служебный API страницы животного (wolf.zoostav.ru). " +
                      "Для служебных разделов: получите токен в POST /api/auth/login и нажмите Authorize."
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите JWT-токен (без слова Bearer)."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        [new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }] = new List<string>()
    });
});

var app = builder.Build();

// ---------- Инициализация БД и демо-данных ----------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    await DbSeeder.SeedAsync(db, scope.ServiceProvider.GetRequiredService<PasswordService>());
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZooStav Wolf API v1");
    c.DocumentTitle = "ZooStav — API северного волка";
});

app.UseStaticFiles();


app.UseMiddleware<SubdomainRoutingMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
ZooApiEndpoints.Map(app);
WebcamEndpoints.Map(app);


app.MapGet("/", async (HttpContext ctx, AppDbContext db, IConfiguration cfg) =>
{
    
    if (ctx.Items.ContainsKey("ZooSubdomain") && !ctx.Items.ContainsKey("ZooAnimalId"))
        return Results.Content(UnknownAnimalHtml(ctx), "text/html; charset=utf-8", System.Text.Encoding.UTF8,
            StatusCodes.Status404NotFound);

    var slug = cfg.GetSection("Zoo:DevSubdomains").Get<string[]>()?.FirstOrDefault() ?? "wolf";
    var animal = await db.Animals.AsNoTracking().OrderBy(a => a.Id).FirstOrDefaultAsync();
    var target = animal != null ? $"/wolf/{animal.SubdomainSlug}" : $"/wolf/{slug}";

    if (ctx.Request.Query.ContainsKey("json") || ctx.Request.Headers.Accept.ToString().Contains("application/json"))
        return Results.Json(new
        {
            application = "ZooStav — страница северного волка",
            mainSite = cfg["Zoo:MainSiteUrl"],
            animalPage = $"{cfg["Zoo:PublicBaseUrl"]}/",
            api = "/api/animals",
            swagger = "/swagger"
        });

    return Results.Redirect(target);
});

app.MapFallback(async (HttpContext ctx) =>
{

    if (ctx.Items.ContainsKey("ZooSubdomain") && !ctx.Items.ContainsKey("ZooAnimalId"))
    {
        ctx.Response.StatusCode = StatusCodes.Status404NotFound;
        ctx.Response.ContentType = "text/html; charset=utf-8";
        await ctx.Response.WriteAsync(UnknownAnimalHtml(ctx));
        return;
    }
    ctx.Response.StatusCode = StatusCodes.Status404NotFound;
});

static string UnknownAnimalHtml(HttpContext ctx)
{
    var slug = ctx.Items["ZooSubdomain"];
    return $$"""
        <!doctype html><html lang="ru"><head><meta charset="utf-8"><title>Животное не найдено</title></head>
        <body style="font-family:system-ui;background:#0d1b2a;color:#e8eef5;padding:40px">
        <h1>Животное «{{slug}}» не найдено</h1>
        <p>Похоже, для поддомена <b>{{slug}}.zoostav.ru</b> ещё не заведена карточка животного.</p>
        <p>Заведите особь в базе и укажите этот слаг в поле <code>Animal.SubdomainSlug</code> — страница появится автоматически.</p>
        <p><a style="color:#7fd1ff" href="https://zoostav.ru">← Вернуться на главную страницу зоопарка</a></p>
        </body></html>
        """;
}

app.Run();

public partial class Program { }   // для интеграционных тестов 
