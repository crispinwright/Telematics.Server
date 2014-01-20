using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web.Helpers;
using Telematics.Server.Data.Json;

namespace Telematics.DataCreator
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var data = File.ReadAllText("sampledata.txt");
			data = data.Replace("[", "").Replace("]", "");
			var dataArray = data.Split(new[] {"},{"}, StringSplitOptions.None);
			var points = new List<Tuple<double, double>>();

			foreach (var s in dataArray)
			{
				var working = s.Replace("{", "").Replace("}", "");
				var workingArray = working.Split(new[] {','});

				var lat = workingArray[0].Split(new[] {':'})[1];
				var lon = workingArray[1].Split(new[] {':'})[1];
				points.Add(new Tuple<double, double>(double.Parse(lat), double.Parse(lon)));
			}

			var interval = 1000*5;
			var groupsize = 1;
//			HttpClient cl = new HttpClient {BaseAddress = new Uri("http://ktelematics.azurewebsites.net/")};

            var handler = new HttpClientHandler
            {
                //                    CookieContainer = cookies,
                //                    UseCookies = true,
                UseDefaultCredentials = false,
                //Credentials = ,
                Proxy = new WebProxy("http://w8dvaklpx01", false, new string[] { },new NetworkCredential("si554437","")),
                UseProxy = true,
            };
            HttpClient cl = new HttpClient(handler) { BaseAddress = new Uri("http://ktelematics3.azurewebsites.net/") };
			//	cl.BaseAddress = new Uri("http://localhost/telematics.server/");
			cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			for (int i = 0; i < points.Count/groupsize; i++)
			{
				var postItem = new GeoMain {DeviceID = 1.ToString(), VehicleID = 1, UserID = 1, sendTime = DateTime.Now};
				var counter = 0;
				var start = i*groupsize;
				postItem.Points = points.Skip(start).Take(groupsize).Select(x =>
				        {
					        var result = new Point()
					                {
						                Lat = points[start + counter].Item1,
						                Lon = points[start + counter].Item2,
						                UTCTime = DateTime.UtcNow.AddYears(1000)
					                };
					        counter++;
					        return result;
				        }).ToList();

				var stringContent = new StringContent(Json.Encode(postItem), Encoding.UTF8, "application/json");

				var postResult = cl.PostAsync("api/geo", stringContent).Result;
				var stringResult = postResult.Content.ReadAsStringAsync().Result;
				Thread.Sleep(interval);
			}
		}
	}
}