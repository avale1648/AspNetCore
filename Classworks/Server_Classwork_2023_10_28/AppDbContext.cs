using Microsoft.EntityFrameworkCore;
//
namespace Server_Classwork_2023_10_28
{
	public class AppDbContext: DbContext
	{
		public DbSet<Order> Orders => Set<Order>();
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
		{ }
	}
}
