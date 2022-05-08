using Klapper.Data;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<MoonrakerApiService>(x =>
{
    var logger = x.GetRequiredService<ILogger<MoonrakerApiService>>();
    const string baseAddress = "http://192.168.8.125";
    return new MoonrakerApiService(logger, baseAddress);
});
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();