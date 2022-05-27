using Klapper.Data;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<MoonrakerApiService>();
builder.Services.AddSingleton<PrinterStatusService>();
builder.Services.AddSingleton<PIDCalibrationService>();
builder.Services.AddSingleton<BedScrewsAdjusterService>();

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

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();