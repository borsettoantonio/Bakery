using System.Globalization;
using Bakery.Services.Application;
using Bakery.Services.Infrastructure;

/* namespace Bakery;

public class Program
{
    public static void Main()
    { */
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
builder.Services.AddTransient<IInitDb, InitDb>();
builder.Services.AddTransient<IProdotti, Prodotti>();
builder.Services.AddSingleton<Store>();
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

/*
Esempio di creazione di un servizio passando un parametro al costruttore.
Preso da learnrazorpage.com alla sezione: Dependency Injection in Razor Pages / Registering a Service with Constructor Parameters 
Inoltre viene mostrato l'uso del parametro s della lambda di tipo IServiceProvider per recuperare un altro servizio
in questo caso IConfiguration
*/
builder.Services.AddSingleton<IConnectionFactory>(s => new SqlConnectionFactory(s.GetService<IConfiguration>().GetSection("ConnectionStrings").GetValue<string>("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

/*
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new(CultureInfo.CurrentCulture),
    SupportedCultures = new[] { CultureInfo.CurrentCulture },
    SupportedUICultures = new[] { CultureInfo.CurrentCulture }
});
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;
*/

app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Query.ContainsKey("stop"))
    {
        await context.Response.WriteAsync("STOP pipeline");
    }
    await next();
});

app.MapStaticAssets().ShortCircuit();

app.Use(async (context, next) =>
{
    if (context.Request.Query.ContainsKey("stop"))
    {
        await context.Response.WriteAsync("STOP pipeline");
    }
    await next();
});

app.MapRazorPages()
   .WithStaticAssets();

/*
// queste istruzioni ritornano una risposta di testo e chiudono la pipeline 
app.Run(async (context) =>
{
    await context.Response.WriteAsync("All done");
});
*/

// prova di custom middleware
app.UseMiddleware<ElapsedTimeMiddleware>();
app.UseMiddleware<MioMiddleware>();

app.Run();

public class ElapsedTimeMiddleware
{
    RequestDelegate _next;
    Store store;
    IConnectionFactory conn;
    public ElapsedTimeMiddleware(RequestDelegate next, Store _store, IConnectionFactory _conn)
    {
        _next = next;
        store = _store;
        conn = _conn;
    }
    public async Task InvokeAsync(HttpContext context, ILogger<ElapsedTimeMiddleware> logger)
    {
        conn.CreateConnection();

        // prova condivisione dati tra middleware usando un servizio (Store)
        store.dato = "ElapsedTimeMiddleware";

        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        await _next(context);
        var isHtml = context.Response.ContentType?.ToLower().Contains("text/html");
        if (context.Response.StatusCode == 200 && isHtml.GetValueOrDefault())
        {
            logger.LogInformation("Primo");
            logger.LogInformation($"{context.Request.Path} executed in {sw.ElapsedMilliseconds}ms");
        }
    }
}

// secondo middleware per scambio dati
public class MioMiddleware
{
    RequestDelegate _next;
    Store store;
    public MioMiddleware(RequestDelegate next, Store _store)
    {
        _next = next;
        store = _store;
    }
    public async Task InvokeAsync(HttpContext context, ILogger<MioMiddleware> logger)
    {
        // prova condivisione dati tra middleware usando un altro middleware (Store)
        logger.LogInformation($"Dato passato: {store.dato}");

        await _next(context);
    }
}

/*    }
} */