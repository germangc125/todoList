using System;
using Microsoft.Owin.Hosting;

namespace TodoApi
{
	public class Program
	{
		protected static void Main(string[] args)
		{
			var port = 8080;
			var url = $"http://localhost:{port}";

			StartOptions options = new StartOptions();
			options.Urls.Add("http://localhost:8080");
			options.Urls.Add("http://127.0.0.1:8080");
			options.Urls.Add(string.Format("http://{0}:8080", Environment.MachineName));

			using (WebApp.Start<Startup>(options))
			{
				Console.WriteLine($"Web Server is running at {url}.");
				Console.WriteLine("Press any key to quit.");
				Console.ReadLine();
			}
		}
	}
}
