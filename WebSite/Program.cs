using MySqlConnector;
using ContosoCrafts.WebSite.Middleware;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Services.DatabaseAcess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!);
services.AddSingleton<IDatabaseAcess, MySqlDatabaseAcess>();
services.AddSingleton<IProductService, DBProductService>();
services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<Handle405Middleware>();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();

app.Run();
