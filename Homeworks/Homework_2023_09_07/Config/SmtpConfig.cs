using System.ComponentModel.DataAnnotations;
namespace Homework_2023_09_07.Config
{
	#pragma warning disable CS8618
	public class SmtpConfig
	{
		[Required] public string Host { get; set; }
		[Range(1, ushort.MaxValue)] public int Port { get; set; }
		[Required] public string Username { get; set; }
		[Required] public string Password { get; set; }
		[Required] public string SenderName { get; set; }
		[EmailAddress] public string SenderEmail { get; set; }
		[Required] public string ReceiverName { get; set; }
		[EmailAddress] public string ReceiverEmail { get; set; }
		[Required] public string Subject { get; set; }
		[Required] public string Body { get; set; }
		[Required] public bool UseSsl { get; set; }
		[Required] public int Retries { get; set; }
	}
}
