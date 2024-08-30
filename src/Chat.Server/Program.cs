using Chat.Server.Database;
using Chat.Server.Extensions;
using Chat.Server.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddAuthorization();

builder.Services.AddSignalrService();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ChatContext>(options=> options.UseNpgsql(builder.Configuration["DatabaseConnStr"]));

builder.Services.AddUserService(options=> options.RequiredLength = 8);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();
