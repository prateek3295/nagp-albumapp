using AlbumService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IAlbumService, AlbumMongoService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecks();

MongoSettings? preciseConfig = builder.Configuration.GetSection("Mongo").Get<MongoSettings>();

var app = builder.Build();
app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


CancellationTokenSource cancellation = new();
app.Lifetime.ApplicationStopping.Register(() =>
{
    cancellation.Cancel();
});



// This API demonstrates how to use task cancellation
// to support graceful container shutdown via SIGTERM.
// The method itself is an example and not useful.
app.MapGet("/Delay/{value}", async (int value) =>
{
    try
    {
        await Task.Delay(value, cancellation.Token);
    }
    catch (TaskCanceledException)
    {
    }

    return new { Delay = value };
});

app.Run();