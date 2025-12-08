using UfcStatsWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// existing services (unchanged)
builder.Services.AddSingleton<IFighterDataService, FighterDataService>();
builder.Services.AddSingleton<IUfcDataService, UfcDataService>();

// new CSV service (added)
builder.Services.AddSingleton<IFightsDataService, FightsCsvService>();

// rankings CSV service
builder.Services.AddSingleton<IRankingsDataService, RankingsCsvService>();

// new fighters CSV service
builder.Services.AddSingleton<IUfcFightersService, UfcFightersCsvService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
