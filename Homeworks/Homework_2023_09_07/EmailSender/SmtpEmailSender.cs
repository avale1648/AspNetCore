using Homework_2023_09_07.Config;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using MimeKit;
using Polly;
using Polly.Retry;
using System.Linq.Expressions;
using System.Threading;
namespace Homework_2023_09_07.EmailSender
{
	public class SmtpEmailSender : IEmailSender, IAsyncDisposable, IDisposable
	{
		private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
		private readonly SmtpConfig _config;
		private readonly SmtpClient _client;
		private readonly ILogger<SmtpEmailSender> _logger;
		private AsyncRetryPolicy _policy;
		private TimeSpan _timeout;
		private bool _isDisposed;
		private int _retries;
		public SmtpEmailSender(IOptionsSnapshot<SmtpConfig> config, ILogger<SmtpEmailSender> logger)
		{
			_config = config.Value ?? throw new ArgumentNullException(nameof(config));
			_client = new SmtpClient();
			_logger = logger;
			_isDisposed = false;
			_retries = _config.Retries;
			_timeout = _config.Timeout;
			_policy = Policy.Handle<Exception>().WaitAndRetryAsync(_config.Retries, t => _timeout * t, (ex, timespan, retry, context) => _logger.LogWarning(ex, "Retrying: {retry}", retry));
		}
		public async Task SendAsync(CancellationToken token)
		{
			var result = await _policy.ExecuteAndCaptureAsync(async () =>
			{
					await ConnectAndAuthenticateAsync(token);
					var message = new MimeMessage();
					message.From.Add(new MailboxAddress(_config.SenderName, _config.SenderEmail));
					message.To.Add(new MailboxAddress(_config.ReceiverName, _config.ReceiverEmail));
					message.Subject = _config.Subject;
					message.Body = new TextPart(MimeKit.Text.TextFormat.Text)
					{
						Text = _config.Body
					};
					var response = await _client.SendAsync(message, token);
					_logger.LogInformation("Server response: {@response}", response);
			});
			if(result.Outcome == OutcomeType.Failure)
			{
				_logger.LogError(result.FinalException, "Error while sending email");
				throw result.FinalException;
			}
		}
		private async Task ConnectAndAuthenticateAsync(CancellationToken token)
		{
			await _semaphore.WaitAsync(token);
			try
			{
				if (!_client.IsConnected)
				{
					await _client.ConnectAsync(_config.Host, _config.Port, _config.UseSsl, token);
					_logger.LogInformation("Client is connected");
				}
				if (!_client.IsAuthenticated)
				{
					await _client.AuthenticateAsync(_config.Username, _config.Password, token);
					_logger.LogInformation("Client is authenticated");
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				_semaphore.Release();
			}
		}
		public async Task DisconnectAsync(bool quit)
		{
			await _semaphore.WaitAsync();
			if (_client.IsConnected)
			{
				await _client.DisconnectAsync(quit);
				_logger.LogInformation("Client is disconnected");
			}
			_semaphore.Release();
		}
		public async ValueTask DisposeAsync()
		{
			await DisposeAsync(true);
		}
		public async Task DisposeAsync(bool isDisposing)
		{
			await _semaphore.WaitAsync();
			if (_isDisposed) return;
			if (isDisposing)
			{
				if (_client.IsConnected)
				{
					await _client.DisconnectAsync(true);
				}
				_client.Dispose();
			}
			_isDisposed = true;
			_semaphore.Release();
			_logger.LogInformation("SmtpEmailSender was disposed");
		}
		public void Dispose()
		{
			DisposeAsync(true).Wait();
		}
	}
}
