using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InterviewProject.WebAgent
{
	public sealed class WebAgent : IWebAgent
	{
		private readonly HttpClientFactory _clientFactory;
		private readonly ILogger _logger;

		public WebAgent(HttpClientFactory clientFactory, ILoggerFactory loggerFactory)
		{
			_clientFactory = clientFactory;
			_logger = loggerFactory.CreateLogger("WebAgent");
		}

		public async Task<string> GetContent(string url)
		{
			try
			{
				var response = await _clientFactory.Client.GetAsync(url);
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				throw;
			}
		}
	}
}
