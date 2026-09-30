using Microsoft.EntityFrameworkCore;
using TallerServidio.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TallerServidioContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TallerServidio")
    )
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
   pattern: "{controller=Turnos}/{action=Crear}/{id?}");

app.Run();