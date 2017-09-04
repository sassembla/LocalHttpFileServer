using System;
using System.Net;
using System.Net.Http;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var listener = new HttpListener(IPAddress.Parse(args[0]), Convert.ToInt32(args[1]));
        var filePathBase = args[2];
		try 
		{
			listener.Request += async (sender, context) => {
				var request = context.Request;
				var response = context.Response;
				if(request.Method.ToString() == HttpMethod.Get.ToString()) 
				{
					Console.WriteLine("request:" + request.RequestUri.LocalPath);
					// await response.WriteContentAsync($"Hello from Server at: {DateTime.Now}\r\n");
					
					/*
						単純にファイルを返す。
						パスから探してあったら、なかったらみたいな感じでいいか。
					*/
					var targetFilePath = filePathBase + request.RequestUri.LocalPath;
					if (File.Exists(targetFilePath)) {
						var bytes = File.ReadAllBytes(targetFilePath);
						await response.WriteContentAsync(bytes);
					} else {
						Console.WriteLine("file not found:" + targetFilePath);
						response.NotFound();
						await response.WriteContentAsync("not found:" + targetFilePath);
					}
					
				}
				else
				{
					response.MethodNotAllowed();
				}

				// Close the HttpResponse to send it back to the client.
				response.Close();
			};
			listener.Start();

			Console.WriteLine("The server is running. Press any key to exit.");
			Console.ReadKey();
		}
		catch(Exception exc) 
		{
			Console.WriteLine(exc.ToString());
		}
		finally 
		{
			listener.Close();
		}

    }
}
