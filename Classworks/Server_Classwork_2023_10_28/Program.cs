using Microsoft.EntityFrameworkCore;
using Server_Classwork_2023_10_28;
//
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("AppDb")));
//
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
app.UseAuthorization();
app.MapRazorPages();
app.MapGet("/orders", async (AppDbContext context) => await context.Orders.ToListAsync());
//app.MapGet("/orders", async (AppDbContext context, int id) => await context.Orders.FirstOrDefaultAsync(o => o.Id == id));
app.MapPost("/orders", async (AppDbContext context, Order order) => 
{
	await context.Orders.AddAsync(order);
	await context.SaveChangesAsync();
});
app.MapPut("/orders", async (AppDbContext context, int id, Order new_) => 
{ 
	var result = await context.Orders.SingleOrDefaultAsync(o => o.Id == id); 
	if(result != null)
	{
		result.Name = new_.Name;
		result.Price = new_.Price;
		await context.SaveChangesAsync();
	}
});
app.MapDelete("/orders", async (AppDbContext context, int id) => 
{
	context.Orders.Remove(context.Orders.SingleOrDefault(o => o.Id == id));
	await context.SaveChangesAsync();
});
app.Run();
