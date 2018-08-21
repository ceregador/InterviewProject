using System.Net;
using System.Net.Http;

namespace InterviewProject.WebAgent
{
	public class HttpClientFactory
	{
		public HttpClientFactory()
		{
			Client = new HttpClient(new HttpClientHandler
			{
				AllowAutoRedirect = true,
				AutomaticDecompression = DecompressionMethods.Deflate,
			});
		}

		public HttpClient Client { get; }
	}
}