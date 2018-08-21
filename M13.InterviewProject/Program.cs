using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace InterviewProject
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseStartup<Startup>()
				.UseIISIntegration()
				.UseApplicationInsights()
				.Build();

			host.Run();
		}
	}
}
