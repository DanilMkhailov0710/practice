using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb17;Trusted_Connection=True;";
string connection2 = "Server=(localdb)\\mssqllocaldb;Database=applicationdb23;Trusted_Connection=True;";
builder.Services.AddDbContext<ApplicationContext1>(options => options.UseSqlServer(connection));
builder.Services.AddDbContext<ApplicationContext2>(options => options.UseSqlServer(connection2));
builder.Services.AddControllers();
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=One_Controller}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Two_Controller}/{action=Index}/{id?}");

app.Run();