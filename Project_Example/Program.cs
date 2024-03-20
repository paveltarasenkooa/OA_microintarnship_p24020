using Microsoft.EntityFrameworkCore;
using OA_Example_Project.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<OaProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseEndpoints(endpoints =>
//{
//    _ = endpoints.MapControllers(); // This enables attribute routing for controllers.
//});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseStaticFiles();
app.Run();
