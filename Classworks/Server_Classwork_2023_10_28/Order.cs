namespace Server_Classwork_2023_10_28
{
	public class Order
	{
		protected Order() { }
		public int Id { get; init; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}