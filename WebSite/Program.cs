using MySqlConnector;
using ContosoCrafts.WebSite.Middleware;
using ContosoCrafts.WebSite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!);
services.AddSingleton<IProductService, MySqlProductService>();
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

//app.UseStatusCodePagesWithReExecute("/errors/{0}"); // For redirection on errors 
app.UseMiddleware<Handle405Middleware>();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();

app.Run();
