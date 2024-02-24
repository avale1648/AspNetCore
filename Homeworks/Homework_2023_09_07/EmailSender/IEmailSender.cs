namespace Homework_2023_09_07.EmailSender
{
	public interface IEmailSender
	{
		public Task SendAsync(CancellationToken token);
	}
}
