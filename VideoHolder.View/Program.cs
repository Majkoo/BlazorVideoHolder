using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using VideoHolder.Core.Factories;
using VideoHolder.Core.Factories.Interfaces;
using VideoHolder.Data;
using VideoHolder.Data.Entities;
using VideoHolder.Data.Repos;
using VideoHolder.Data.Repos.Interfaces;
using VideoHolder.View.Auth;
using VideoHolder.View.Auth.Interfaces;

var builder = WebApplication.CreateBuilder(args);




#region DataServices

builder.Services.AddScoped<IItemRepo, ItemRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();

#endregion

#region CoreServices

builder.Services.AddScoped<IAuthHelper, AuthHelper>();
builder.Services.AddScoped<IItemFactory, ItemFactory>();

#endregion

#region ViewServices

    builder.Services.AddScoped<NotificationService>();
    builder.Services.AddScoped<DialogService>();

#endregion

#region GeneralServices

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddDbContext<HolderDbContext>(opts =>
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    opts.UseMySql(connString, ServerVersion.AutoDetect(connString));
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

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();