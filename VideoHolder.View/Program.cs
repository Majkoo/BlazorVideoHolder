using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Radzen;
using VideoHolder.Core.Factories;
using VideoHolder.Core.Factories.Interfaces;
using VideoHolder.Data;
using VideoHolder.Data.Repos;
using VideoHolder.Data.Repos.Interfaces;

var builder = WebApplication.CreateBuilder(args);


#region DataServices

builder.Services.AddScoped<IItemRepo, ItemRepo>();

#endregion

#region CoreServices

builder.Services.AddScoped<IItemFactory, ItemFactory>();

#endregion

#region ViewServices


    #region RadzenServices

    builder.Services.AddScoped<NotificationService>();
    builder.Services.AddScoped<DialogService>();

    #endregion

#endregion

#region GeneralServices

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<HolderDbContext>(opts =>
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    opts.UseMySql(connString, ServerVersion.AutoDetect(connString),
        x => x.MigrationsAssembly("VideoHolder.View"));
});

#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();